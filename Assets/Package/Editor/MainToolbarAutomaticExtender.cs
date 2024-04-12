﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    [InitializeOnLoad]
    public static class MainToolbarAutomaticExtender
    {
        private class HiddenRemovedElement
        {
            public VisualElement RemovedVisualElement;
            public VisualElement Parent;
            public int Index;
        }

        private static readonly string[] EXCEPTIONAL_ELEMENTS_FOR_VISIBILITY =
        {
            UnityNativeElementsOverrideIds.ACCOUNT_DROPDOWN_ID,
            UnityNativeElementsOverrideIds.CLOUD_BUTTON_ID
        };

        private static MainToolbarElement[] _mainToolbarElements = new MainToolbarElement[0];
        private static GroupElement[] _groupElements = new GroupElement[0];
        private static GroupDefinition[] _groupDefinitions = new GroupDefinition[0];
        private static MainToolbarElement[] _rootElements = new MainToolbarElement[0];
        private static VisualElement[] _nativeElements = new VisualElement[0];
        private static Dictionary<string, HiddenRemovedElement> _hiddenElementsByRemotion = new Dictionary<string, HiddenRemovedElement>();

        private static Dictionary<string, MainToolbarElementOverride> _nativeElementsInitialState = new Dictionary<string, MainToolbarElementOverride>();

        internal static MainToolbarCustomContainer LeftCustomContainer { get; private set; } = new MainToolbarCustomContainer("ToolbarAutomaticExtenderLeftContainer", FlexDirection.RowReverse);
        internal static MainToolbarCustomContainer RightCustomContainer { get; private set; } = new MainToolbarCustomContainer("ToolbarAutomaticExtenderRightContainer", FlexDirection.Row);

        internal static MainToolbarElement[] CustomMainToolbarElements => _mainToolbarElements.ToArray();
        internal static VisualElement[] GroupElements => _groupElements.ToArray();
        internal static VisualElement[] NativeElements => _nativeElements.ToArray();

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
                SaveNativeElementsInitialState();
                SetNativeElementsDefaultState();
            }
        }

        internal static void Refresh()
        {
            if (!MainToolbar.IsAvailable)
                return;

            ResetCustomContainers();
            BuildCustomToolbarContainers();
            SetNativeElementsDefaultState();
            ApplyOverridesOnNativeElements();
            OnRefresh?.Invoke();
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
            _rootElements = GetRootElements();
            _groupElements = GetGroupElements();

            ApplyOverridesOnCustomElements();
            ApplyRecommendedStylesOnRootSingles();

            AddRootElementsToContainers();
        }

        private static void ApplyRecommendedStylesOnRootSingles()
        {
            var eligibleRootSingles = GetRootElements()
                .Where(element => element.UseRecommendedStyles);

            foreach(var rootSingle in eligibleRootSingles)
            {
                RecommendedStyles.Apply(rootSingle.VisualElement);
            }
        }

        private static GroupElement[] GetGroupElements()
        {
            return _rootElements
                .Where(rootElement => rootElement.VisualElement is GroupElement)
                .Select(rootElement => rootElement.VisualElement as GroupElement)
                .ToArray();
        }

        private static MainToolbarElement[] GetRootElements()
        {
            return GetGroups()
                .Concat(GetSingles())
                .ToArray();
        }

        private static void ApplyOverridesOnCustomElements()
        {
            var allElements = CustomMainToolbarElements.Select(e => e.VisualElement)
                .Concat(GroupElements);

            foreach(var element in allElements)
            {
                ApplyOverride(element);
            }
        }

        private static void SaveNativeElementsInitialState()
        {
            if (_nativeElementsInitialState.Count > 0)
                return;

            foreach (var element in _nativeElements)
            {
                var overrideId = MainToolbarElementOverrideIdProvider.IdOf(element);

                _nativeElementsInitialState[overrideId] =
                    new MainToolbarElementOverride(
                        overrideId,
                        element.resolvedStyle.display == DisplayStyle.Flex
                        );
            }
        }

        private static void SetNativeElementsDefaultState()
        {
            foreach (var element in _nativeElements)
            {
                var overrideId = MainToolbarElementOverrideIdProvider.IdOf(element);
                var defaultStateOverride = _nativeElementsInitialState[overrideId];

                ApplyOverride(element, defaultStateOverride);
            }    
        }

        private static void ApplyOverridesOnNativeElements()
        {
            foreach(var nativeElement in _nativeElements)
            {
                ApplyOverride(nativeElement);
            }
        }

        private static VisualElement[] GetNativeElements()
        {
            return MainToolbar.LeftContainer.Children()
                .Concat(MainToolbar.RightContainer.Children())
                .Concat(MainToolbar.PlayModeButtonsContainer.Children())
                .Where(visualElement => UnityNativeElementsOverrideIds.IdOf(visualElement) != null)
                .ToArray();
        }

        private static void ApplyOverride(VisualElement visualElement)
        {
            var elementOverrideId = MainToolbarElementOverrideIdProvider.IdOf(visualElement);

            if (elementOverrideId == null)
                return;

            var userOverride = ServicesAndRepositories.MainToolbarElementOverridesRepository.Get(elementOverrideId);

            if (userOverride == null)
                return;

            ApplyVisibilityOverride(elementOverrideId, visualElement, userOverride.Value.Visible);
        }

        private static void ApplyOverride(VisualElement visualElement, MainToolbarElementOverride overrideData)
        {
            var elementOverrideId = MainToolbarElementOverrideIdProvider.IdOf(visualElement);

            if (elementOverrideId == null)
                return;

            ApplyVisibilityOverride(elementOverrideId, visualElement, overrideData.Visible);
        }

        private static void ApplyVisibilityOverride(string elementId, VisualElement visualElement, bool visible)
        {
            if(IsExceptionElementForVisibility(elementId))
                HandleVisibilityApplicationOnExceptions(elementId, visualElement, visible);
            else
                visualElement.style.display = visible ? DisplayStyle.Flex : DisplayStyle.None;
        }

        private static bool IsExceptionElementForVisibility(string elementId)
        {
            return EXCEPTIONAL_ELEMENTS_FOR_VISIBILITY.Contains(elementId);
        }

        private static void HandleVisibilityApplicationOnExceptions(string elementId, VisualElement visualElement, bool visible)
        {
            if (visible)
                HandleExceptionalElementVisibleCase(elementId, visualElement);
            else
                HandleExceptionalElementInvisibleCase(elementId, visualElement);
        }

        private static void HandleExceptionalElementInvisibleCase(string elementId, VisualElement visualElement)
        {
            var parent = visualElement.parent;
            var index = parent.IndexOf(visualElement);

            if (parent.Contains(visualElement))
                parent.Remove(visualElement);

            if (!_hiddenElementsByRemotion.ContainsKey(elementId))
                _hiddenElementsByRemotion.Add(elementId, new HiddenRemovedElement()
                {
                    RemovedVisualElement = visualElement,
                    Parent = parent,
                    Index = index
                });
        }

        private static void HandleExceptionalElementVisibleCase(string elementId, VisualElement visualElement)
        {
            if (_hiddenElementsByRemotion.ContainsKey(elementId))
            {
                var removedElement = _hiddenElementsByRemotion[elementId];

                var parent = removedElement.Parent;

                if (!parent.Contains(visualElement))
                {
                    var bestIndex = GetIndexEqualOrLessThan(removedElement.Index, parent);

                    parent.Insert(bestIndex, visualElement);
                }

                _hiddenElementsByRemotion.Remove(elementId);
            }
        }

        private static int GetIndexEqualOrLessThan(int removedElementIndex, VisualElement parent)
        {
            for (int i = parent.childCount - 1; i >= 0; i--)
            {
                if (removedElementIndex <= i)
                    return i;
            }

            return 0;
        }

        private static void ApplyFixedChangesToToolbar()
        {
            ConfigureStyleOfContainers();
            AddCustomContainers();
            ApplyOverridesOnNativeElements();
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
            var elementsByGroup = GetElementsByGroup();

            foreach (var groupDefinition in _groupDefinitions)
            {
                var elementsOfThisGroup = elementsByGroup[groupDefinition.GroupName];

                if (elementsOfThisGroup.Count == 0)
                    continue;

                var groupToolbarElement = new MainToolbarElement(
                    new GroupElement(
                        groupDefinition.GroupName,
                        elementsOfThisGroup
                            .Select(el => el.VisualElement)
                            .ToArray()),
                    groupDefinition.Alignment,
                    groupDefinition.Order,
                    false
                    );

                groups.Add(groupToolbarElement);
            }

            return groups.ToArray();
        }

        private static MainToolbarElement[] GetSingles()
        {
            var elementsByGroup = GetElementsByGroup();
            var elementsInGroups = elementsByGroup.Values.SelectMany(list => list);

            return _mainToolbarElements
                .Where(mainToolbarElement => !elementsInGroups.Contains(mainToolbarElement))
                .ToArray();
        }

        private static Dictionary<string, List<MainToolbarElement>> GetElementsByGroup()
        {
            var elementsByGroup = new Dictionary<string, List<MainToolbarElement>>();

            foreach (var groupDefinition in _groupDefinitions)
            {
                var elementsOfThisGroup = groupDefinition.ToolbarElementsTypes
                    .Select(type => _mainToolbarElements
                        .FirstOrDefault(element => element.VisualElement.GetType().FullName == type)
                        )
                    .Where(mainToolbarElement => mainToolbarElement != null)
                    .ToList();
                elementsByGroup.Add(groupDefinition.GroupName, elementsOfThisGroup);
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