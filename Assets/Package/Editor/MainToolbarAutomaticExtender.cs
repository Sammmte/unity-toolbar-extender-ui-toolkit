using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using System.Reflection;
using UnityEngine;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    [InitializeOnLoad]
    public static class MainToolbarAutomaticExtender
    {
        private static MainToolbarElement[] _mainToolbarElements = new MainToolbarElement[0];
        private static MainToolbarElement[] _groupElements = new MainToolbarElement[0];
        private static GroupDefinition[] _groupDefinitions = new GroupDefinition[0];
        private static MainToolbarElement[] _rootElements = new MainToolbarElement[0];
        private static MainToolbarElement[] _singleElements = new MainToolbarElement[0];
        private static MainToolbarElement[] _orphanElements = new MainToolbarElement[0];
        private static MainToolbarElement[] _validRootElements = new MainToolbarElement[0];
        private static MainToolbarElementOverrideApplier _overrideApplier = new MainToolbarElementOverrideApplier(ServicesAndRepositories.MainToolbarElementOverridesRepository);
        private static Dictionary<string, MainToolbarElement[]> _elementsByGroup = new Dictionary<string, MainToolbarElement[]>();
        private static MainToolbarElementVariableWatcher _variableWatcher = new MainToolbarElementVariableWatcher(ServicesAndRepositories.MainToolbarElementVariableRepository, ServicesAndRepositories.ValueSerializer);

        internal static MainToolbarElement[] CustomMainToolbarElements => _mainToolbarElements.ToArray();
        internal static MainToolbarElement[] GroupElements => _groupElements.ToArray();
        internal static MainToolbarElement[] OrphanElements => _orphanElements.ToArray();

        public static event Action OnRefresh;

        static MainToolbarAutomaticExtender()
        {
            MainToolbar.OnInitialized += Initialize;
        }

        private static void Initialize()
        {
            BuildCustomToolbarContainers();

            if (_mainToolbarElements.Length == 0)
                return;

            EditorApplication.projectChanged += OnProjectChange;
            MainToolbar.OnRefresh += ReApplyChangesToToolbar;

            ReApplyChangesToToolbar();
        }

        internal static void Refresh()
        {
            if (!MainToolbar.IsAvailable)
                return;

            BuildCustomToolbarContainers();
            _overrideApplier.ApplyOverrides();
            OnRefresh?.Invoke();
        }

        private static void RefreshRootElements()
        {
            ClearDummyToolbarElements();
            AddRootElementsToContainers();
        }

        internal static MainToolbarElement[] GetElementsOfGroup(string id)
        {
            return _elementsByGroup[id].ToArray();
        }

        private static void BuildCustomToolbarContainers()
        {
            _mainToolbarElements = ServicesAndRepositories.MainToolbarElementRepository.GetAll();

            if (_mainToolbarElements.Length == 0)
                return;

            _groupDefinitions = LoadGroupDefinitions();
            _groupElements = GetGroups();
            _elementsByGroup = GetElementsByGroup();
            InitializeGroups();
            _singleElements = GetSingles();
            _rootElements = GetRootElements();
            _orphanElements = GetOrphanElements();
            _validRootElements = GetValidRootElements();
            _overrideApplier.SetCustomElements(_mainToolbarElements.Concat(_groupElements).ToArray());
            _variableWatcher.RestoreValues(CustomMainToolbarElements);

            InitializeCustomElements();
            RegisterElementsForRecommendedStyles();

            ClearDummyToolbarElements();
            AddRootElementsToContainers();

            EditorApplication.update -= Update;
            EditorApplication.update += Update;
        }

        private static void InitializeCustomElements()
        {
            foreach(var customElement in CustomMainToolbarElements)
            {
                var typeOfElement = customElement.VisualElement.GetType();

                var initializeMethod = typeOfElement.GetMethod("InitializeElement", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                initializeMethod?.Invoke(customElement.VisualElement, null);
            }
        }

        private static void Update()
        {
            _variableWatcher.Update();
        }

        private static void InitializeGroups()
        {
            foreach (var group in _groupElements)
            {
                var groupElement = group.VisualElement as GroupElement;
                var elementsOfThisGroup = _elementsByGroup[group.Id]
                    .Select(e => e.VisualElement)
                    .ToArray();

                groupElement.Initialize(elementsOfThisGroup);
            }
        }

        private static void RegisterElementsForRecommendedStyles()
        {
            var eligibleElements = _mainToolbarElements
                .Concat(_groupElements)
                .Where(element => element.UseRecommendedStyles)
                .Select(element => new RecommendedStyleVisualElement(element.VisualElement, IsInGroup(element)))
                .ToArray();

            RecommendedStyles.SetElements(eligibleElements);
        }

        private static bool IsInGroup(MainToolbarElement mainToolbarElement)
        {
            if (_orphanElements.Contains(mainToolbarElement))
                return true;
            
            return !_rootElements.Contains(mainToolbarElement);
        }

        private static MainToolbarElement[] GetRootElements()
        {
            return GetRootGroups()
                .Concat(_singleElements)
                .ToArray();
        }

        private static MainToolbarElement[] GetOrphanElements()
        {
            return _rootElements
                .Where(e => !MainToolbar.CanBeReplaced(e))
                .ToArray();
        }

        private static MainToolbarElement[] GetValidRootElements()
        {
            return _rootElements
                .Where(e => !_orphanElements.Contains(e))
                .ToArray();
        }

        private static MainToolbarElement[] GetRootGroups()
        {
            var rootGroups = new List<MainToolbarElement>();

            var allGroupElements = _groupElements.Select(group => group.VisualElement as GroupElement);

            foreach (var group in _groupElements)
            {
                var groupElement = group.VisualElement as GroupElement;

                if (!allGroupElements.Any(g => g.GroupedElements.Contains(groupElement)))
                    rootGroups.Add(group);
            }

            return rootGroups.ToArray();
        }

        private static void ReApplyChangesToToolbar()
        {
            _overrideApplier.ApplyOverrides();
            AddRootElementsToContainers();
        }

        private static void ClearDummyToolbarElements()
        {
            foreach (var element in _mainToolbarElements)
            {
                MainToolbar.TryClearDummyToolbarElement(element.Id);
            }

            foreach (var element in _groupElements)
            {
                MainToolbar.TryClearDummyToolbarElement(element.Id);
            }
        }

        private static void AddRootElementsToContainers()
        {
            foreach (var element in _validRootElements)
            {
                MainToolbar.TryReplace(element);
            }
        }

        private static MainToolbarElement[] GetGroups()
        {
            var groups = new List<MainToolbarElement>();

            foreach (var groupDefinition in _groupDefinitions)
            {
                if (groupDefinition.ToolbarElementsIds.Length == 0)
                    continue;

                var groupToolbarElement = new MainToolbarElement(
                    groupDefinition.GroupId,
                    new GroupElement(groupDefinition.GroupName),
                    groupDefinition.Order,
                    true
                    );

                groups.Add(groupToolbarElement);
            }

            return groups.ToArray();
        }

        private static MainToolbarElement[] GetSingles()
        {
            var elementsInGroups = _elementsByGroup.Values.SelectMany(list => list);

            return _mainToolbarElements
                .Where(mainToolbarElement => !elementsInGroups.Contains(mainToolbarElement))
                .ToArray();
        }

        private static Dictionary<string, MainToolbarElement[]> GetElementsByGroup()
        {
            var elementsByGroup = new Dictionary<string, MainToolbarElement[]>();

            foreach (var groupDefinition in _groupDefinitions)
            {
                var elementsOfThisGroup = groupDefinition.ToolbarElementsIds
                    .Select(id =>
                    {
                        var element = _mainToolbarElements.FirstOrDefault(e => e.Id == id);

                        if (element != null)
                            return element;

                        element = _groupElements.FirstOrDefault(e => e.Id == id);

                        return element;
                    })
                    .Where(mainToolbarElement => mainToolbarElement != null)
                    .ToList();

                elementsByGroup.Add(groupDefinition.GroupId, elementsOfThisGroup.ToArray());
            }

            return elementsByGroup;
        }

        private static GroupDefinition[] LoadGroupDefinitions()
        {
            return ServicesAndRepositories.GroupDefinitionRepository.GetAll();
        }

        private static void OnProjectChange()
        {
            if (ShouldRefresh())
            {
                ClearDummyToolbarElements();
                Refresh();
            }
        }

        private static bool ShouldRefresh()
        {
            return GroupsChanged();
        }

        private static bool GroupsChanged()
        {
            var groups = ServicesAndRepositories.GroupDefinitionRepository.GetAll();

            if (_groupDefinitions.Length != groups.Length)
                return true;

            if (_groupDefinitions.Length == 0 && groups.Length == 0)
                return false;

            for(int i = 0; i < _groupDefinitions.Length; i++)
            {
                var savedGroupDefinition = _groupDefinitions[i];
                var retrievedGroupDefinition = groups[i];

                if (!savedGroupDefinition.AreEquals(retrievedGroupDefinition))
                    return true;
            }

            return false;
        }

        internal static void ShowOrphanElementsGroup(Rect rect)
        {
            if (OrphanElements.Length == 0)
            {
                Debug.LogWarning("No visual elements with Paps MainToolbarElementAttribute were found. You need create some. Check the docs.");
                return;
            }
            
            GroupDropdownWindowPopupManager.Show(rect,
                OrphanElements.Select(e => e.VisualElement).ToArray());
            
            RefreshRootElements();
        }
    }
}
