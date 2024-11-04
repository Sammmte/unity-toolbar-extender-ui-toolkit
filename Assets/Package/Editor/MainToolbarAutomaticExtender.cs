using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;
using System.Reflection;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    [InitializeOnLoad]
    public static class MainToolbarAutomaticExtender
    {
        private static MainToolbarElement[] _mainToolbarElements = new MainToolbarElement[0];
        private static MainToolbarElement[] _groupElements = new MainToolbarElement[0];
        private static GroupDefinition[] _groupDefinitions = new GroupDefinition[0];
        private static MainToolbarElement[] _rootElements = new MainToolbarElement[0];
        private static NativeToolbarElement[] _nativeElements = new NativeToolbarElement[0];
        private static MainToolbarElement[] _singleElements = new MainToolbarElement[0];
        private static MainToolbarElementOverrideApplier _overrideApplier = new MainToolbarElementOverrideApplier(ServicesAndRepositories.MainToolbarElementOverridesRepository);
        private static Dictionary<string, MainToolbarElement[]> _elementsByGroup = new Dictionary<string, MainToolbarElement[]>();
        private static MainToolbarElementVariableWatcher _variableWatcher = new MainToolbarElementVariableWatcher(ServicesAndRepositories.MainToolbarElementVariableRepository, ServicesAndRepositories.ValueSerializer);

        internal static MainToolbarCustomContainer LeftCustomContainer { get; private set; } = new MainToolbarCustomContainer("ToolbarAutomaticExtenderLeftContainer", FlexDirection.RowReverse);
        internal static MainToolbarCustomContainer RightCustomContainer { get; private set; } = new MainToolbarCustomContainer("ToolbarAutomaticExtenderRightContainer", FlexDirection.Row);

        internal static MainToolbarElement[] CustomMainToolbarElements => _mainToolbarElements.ToArray();
        internal static MainToolbarElement[] GroupElements => _groupElements.ToArray();
        internal static NativeToolbarElement[] NativeElements => _nativeElements.ToArray();

        public static event Action OnRefresh;
        public static event Action OnAddedCustomContainersToToolbar;

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
            MainToolbar.OnRefresh += ApplyFixedChangesToToolbar;

            CacheNativeElements();
            ApplyFixedChangesToToolbar();
        }

        private static void CacheNativeElements()
        {
            var nativeElements = GetNativeElements();
            if (nativeElements.Length != 0)
            {
                _nativeElements = nativeElements;
                _overrideApplier.SetNativeElements(_nativeElements);
            }
        }

        internal static void Refresh()
        {
            if (!MainToolbar.IsAvailable)
                return;

            ResetCustomContainers();
            BuildCustomToolbarContainers();
            _overrideApplier.ApplyOverrides();
            OnRefresh?.Invoke();
        }

        internal static MainToolbarElement[] GetElementsOfGroup(string id)
        {
            return _elementsByGroup[id].ToArray();
        }

        private static void ResetCustomContainers()
        {
            LeftCustomContainer.ClearContainer();
            RightCustomContainer.ClearContainer();
        }

        private static void BuildCustomToolbarContainers()
        {
            _mainToolbarElements = ServicesAndRepositories.MainToolbarElementRepository.GetAll();

            if (_mainToolbarElements.Count() == 0)
                return;

            _groupDefinitions = LoadGroupDefinitions();
            _groupElements = GetGroups();
            _elementsByGroup = GetElementsByGroup();
            InitializeGroups();
            _singleElements = GetSingles();
            _rootElements = GetRootElements();
            _overrideApplier.SetCustomElements(_mainToolbarElements.Concat(_groupElements).ToArray());
            _variableWatcher.RestoreValues(CustomMainToolbarElements);

            InitializeCustomElements();
            RegisterElementsForRecommendedStyles();

            AddRootElementsToContainers();

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
            return !_rootElements.Contains(mainToolbarElement);
        }

        private static MainToolbarElement[] GetRootElements()
        {
            return GetRootGroups()
                .Concat(_singleElements)
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

        private static NativeToolbarElement[] GetNativeElements()
        {
            return MainToolbar.LeftContainer.Children()
                .Concat(MainToolbar.RightContainer.Children())
                .Concat(MainToolbar.PlayModeButtonsContainer.Children())
                .Where(visualElement => UnityNativeElementsIds.IdOf(visualElement) != null)
                .Select(visualElement => new NativeToolbarElement(UnityNativeElementsIds.IdOf(visualElement), visualElement))
                .ToArray();
        }

        private static void ApplyFixedChangesToToolbar()
        {
            ConfigureStyleOfContainers();
            AddCustomContainers();
            _overrideApplier.ApplyOverrides();
            OnAddedCustomContainersToToolbar?.Invoke();
        }

        private static void AddCustomContainers()
        {
            MainToolbar.CenterContainer.Insert(0, LeftCustomContainer);
            MainToolbar.CenterContainer.Add(RightCustomContainer);
        }

        private static void AddRootElementsToContainers()
        {
            var leftElements = _rootElements.Where(el => el.Alignment == ToolbarAlign.Left)
                .OrderBy(el => el.Order);

            var rightElements = _rootElements.Where(el => el.Alignment == ToolbarAlign.Right)
                .OrderBy(el => el.Order);

            foreach (var orderedAlignedElement in leftElements)
                LeftCustomContainer.AddToContainer(orderedAlignedElement.VisualElement);

            foreach (var orderedAlignedElement in rightElements)
                RightCustomContainer.AddToContainer(orderedAlignedElement.VisualElement);
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
                    groupDefinition.Alignment,
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

        private static void ConfigureStyleOfContainers()
        {
            MainToolbar.LeftContainer.style.flexGrow = 0;
            MainToolbar.LeftContainer.style.width = Length.Auto();

            MainToolbar.RightContainer.style.flexGrow = 0;
            MainToolbar.RightContainer.style.width = Length.Auto();

            MainToolbar.CenterContainer.style.flexGrow = 1;
        }

        private static void OnProjectChange()
        {
            if(ShouldRefresh())
                Refresh();
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
    }
}