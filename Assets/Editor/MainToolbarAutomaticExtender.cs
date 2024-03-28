using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    [InitializeOnLoad]
    public static class MainToolbarAutomaticExtender
    {
        private class RootMainToolbarElement
        {
            public VisualElement VisualElement;
            public ToolbarAlign Alignment;
            public int Order;
        }

        private class MainToolbarElement
        {
            public VisualElement VisualElement;
            public Type DecoratedType;
            public MainToolbarElementAttribute Attribute;
        }

        private static readonly Length TOOLBAR_LEFT_CONTAINER_MIN_WIDTH = 130;
        private static readonly Length TOOLBAR_RIGHT_CONTAINER_MIN_WIDTH = 288;

        internal static Type[] MainToolbarElementTypesInProject { get; private set; } = new Type[0];

        private static MainToolbarElement[] _mainToolbarElements = new MainToolbarElement[0];
        private static GroupElement[] _groupElements = new GroupElement[0];
        private static GroupDefinition[] _groupDefinitions = new GroupDefinition[0];
        private static RootMainToolbarElement[] _rootElements = new RootMainToolbarElement[0];

        private static Dictionary<string, MainToolbarElementOverride> _nativeElementsInitialState = new Dictionary<string, MainToolbarElementOverride>();

        public static MainToolbarCustomContainer LeftCustomContainer { get; private set; } = new MainToolbarCustomContainer("ToolbarAutomaticExtenderLeftContainer", FlexDirection.RowReverse);
        public static MainToolbarCustomContainer RightCustomContainer { get; private set; } = new MainToolbarCustomContainer("ToolbarAutomaticExtenderRightContainer", FlexDirection.Row);

        public static VisualElement[] CustomMainToolbarElements => _mainToolbarElements.Select(m => m.VisualElement).ToArray();
        public static VisualElement[] GroupElements => _groupElements.ToArray();

        public static event Action OnRefresh;
        public static event Action OnAddedCustomContainersToToolbar;

        static MainToolbarAutomaticExtender()
        {
            EditorApplication.projectChanged += OnProjectChange;

            BuildCustomToolbarContainers();

            if (_mainToolbarElements.Length == 0)
                return;

            ToolbarWrapper.OnNativeToolbarWrapped += () =>
            {
                ApplyFixedChangesToToolbar();
                OnAddedCustomContainersToToolbar?.Invoke();
            };
        }

        public static void Refresh()
        {
            if (!ToolbarWrapper.IsAvailable)
                return;

            ResetCustomContainers();
            BuildCustomToolbarContainers();
            ManageOverridesOnNativeElements();
            OnRefresh?.Invoke();
        }

        private static void ResetCustomContainers()
        {
            LeftCustomContainer.ClearContainer();
            RightCustomContainer.ClearContainer();
        }

        private static void BuildCustomToolbarContainers()
        {
            _mainToolbarElements = GetMainToolbarElements();

            if (_mainToolbarElements.Count() == 0)
                return;

            _groupDefinitions = LoadGroupDefinitions();
            _rootElements = GetRootElements();
            _groupElements = GetGroupElements();

            ApplyOverridesOnCustomElements();

            AddRootElementsToContainers();
        }

        private static GroupElement[] GetGroupElements()
        {
            return _rootElements
                .Where(rootElement => rootElement.VisualElement is GroupElement)
                .Select(rootElement => rootElement.VisualElement as GroupElement)
                .ToArray();
        }

        private static RootMainToolbarElement[] GetRootElements()
        {
            return GetGroups()
                .Concat(GetSingles())
                .ToArray();
        }

        private static void ApplyOverridesOnCustomElements()
        {
            var allElements = CustomMainToolbarElements.Concat(GroupElements);

            foreach(var element in allElements)
            {
                ApplyOverride(element);
            }
        }

        private static void SaveNativeElementsInitialState(VisualElement[] nativeElements)
        {
            if (_nativeElementsInitialState.Count > 0)
                return;

            foreach (var element in nativeElements)
            {
                var overrideId = MainToolbarElementOverrideIdProvider.IdOf(element);

                _nativeElementsInitialState[overrideId] =
                    new MainToolbarElementOverride(
                        overrideId,
                        element.resolvedStyle.display == DisplayStyle.Flex
                        );
            }
        }

        private static void SetNativeElementsDefaultState(VisualElement[] nativeElements)
        {
            SaveNativeElementsInitialState(nativeElements);

            foreach (var element in nativeElements)
            {
                var overrideId = MainToolbarElementOverrideIdProvider.IdOf(element);
                var defaultStateOverride = _nativeElementsInitialState[overrideId];

                element.style.display = defaultStateOverride.Visible ? DisplayStyle.Flex : DisplayStyle.None;
            }    
        }

        private static void ApplyOverridesOnNativeElements(VisualElement[] nativeElements)
        {
            foreach(var nativeElement in nativeElements)
            {
                ApplyOverride(nativeElement);
            }
        }

        private static VisualElement[] GetNativeElements()
        {
            return ToolbarWrapper.LeftContainer.Children()
                .Concat(ToolbarWrapper.RightContainer.Children())
                .Concat(ToolbarWrapper.PlayModeButtonsContainer.Children())
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

            visualElement.style.display = userOverride.Value.Visible ? DisplayStyle.Flex : DisplayStyle.None;
        }

        private static void ApplyFixedChangesToToolbar()
        {
            ConfigureStyleOfContainers();
            AddCustomContainers();
            ManageOverridesOnNativeElements();
        }

        private static void ManageOverridesOnNativeElements()
        {
            var nativeElements = GetNativeElements();
            SetNativeElementsDefaultState(nativeElements);
            ApplyOverridesOnNativeElements(nativeElements);
        }

        private static void AddCustomContainers()
        {
            ToolbarWrapper.CenterContainer.Insert(0, LeftCustomContainer);
            ToolbarWrapper.CenterContainer.Add(RightCustomContainer);
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

        private static RootMainToolbarElement[] GetGroups()
        {
            var groups = new List<RootMainToolbarElement>();
            var elementsByGroup = GetElementsByGroup();

            foreach (var groupDefinition in _groupDefinitions)
            {
                var elementsOfThisGroup = elementsByGroup[groupDefinition.GroupName];

                if (elementsOfThisGroup.Count == 0)
                    continue;

                groups.Add(new RootMainToolbarElement()
                {
                    VisualElement = new GroupElement(groupDefinition.GroupName, elementsOfThisGroup.Select(el => el.VisualElement).ToArray()),
                    Alignment = groupDefinition.Alignment,
                    Order = groupDefinition.Order,
                });
            }

            return groups.ToArray();
        }

        private static RootMainToolbarElement[] GetSingles()
        {
            var elementsByGroup = GetElementsByGroup();
            var elementsInGroups = elementsByGroup.Values.SelectMany(list => list);

            return _mainToolbarElements
                .Where(mainToolbarElement => !elementsInGroups.Contains(mainToolbarElement))
                .Select(mainToolbarElement => new RootMainToolbarElement()
                {
                    Alignment = mainToolbarElement.Attribute.AlignWhenSingle,
                    Order = mainToolbarElement.Attribute.Order,
                    VisualElement = mainToolbarElement.VisualElement
                })
                .ToArray();
        }

        private static Dictionary<string, List<MainToolbarElement>> GetElementsByGroup()
        {
            var elementsByGroup = new Dictionary<string, List<MainToolbarElement>>();

            foreach (var groupDefinition in _groupDefinitions)
            {
                elementsByGroup.Add(groupDefinition.GroupName, new List<MainToolbarElement>());
            }

            foreach (var element in _mainToolbarElements)
            {
                var containingGroupDefinition = _groupDefinitions
                    .Where(groupDefinition => groupDefinition.ToolbarElementsTypes.
                        Contains(element.DecoratedType.FullName))
                    .FirstOrDefault();

                if (containingGroupDefinition != null)
                    elementsByGroup[containingGroupDefinition.GroupName].Add(element);
            }

            return elementsByGroup;
        }

        private static GroupDefinition[] LoadGroupDefinitions()
        {
            var paths = AssetDatabase.FindAssets("t:" + nameof(GroupDefinition))
                .Select(guid => AssetDatabase.GUIDToAssetPath(guid));

            return paths
                .Select(path => AssetDatabase.LoadAssetAtPath<GroupDefinition>(path))
                .Where(groupDefinition => !string.IsNullOrEmpty(groupDefinition.GroupName))
                .GroupBy(groupDefinition => groupDefinition.GroupName)
                .Select(group => group.First())
                .ToArray();
        }

        private static void ConfigureStyleOfContainers()
        {
            ToolbarWrapper.LeftContainer.style.flexGrow = 0;
            ToolbarWrapper.LeftContainer.style.width = Length.Auto();

            ToolbarWrapper.RightContainer.style.flexGrow = 0;
            ToolbarWrapper.RightContainer.style.width = Length.Auto();

            ToolbarWrapper.CenterContainer.style.flexGrow = 1;
        }

        private static MainToolbarElement[] GetMainToolbarElements()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .ToArray();

            MainToolbarElementTypesInProject = GetMainToolbarElementTaggedTypes(types);

            return GetMainToolbarElementsFromTypes();
        }

        private static Type[] GetMainToolbarElementTaggedTypes(IEnumerable<Type> allTypes)
        {
            return allTypes
                .Where(type => IsValidVisualElementType(type))
                .ToArray();
        }

        private static MainToolbarElement[] GetMainToolbarElementsFromTypes()
        {
            return MainToolbarElementTypesInProject
                .Select(type => GetMainToolbarElementFromType(type))
                .ToArray();
        }

        private static MainToolbarElement GetMainToolbarElementFromType(Type type)
        {
            var elementInstance = (VisualElement)Activator.CreateInstance(type);
            var attribute = type.GetCustomAttribute<MainToolbarElementAttribute>();

            if (string.IsNullOrEmpty(elementInstance.name))
                elementInstance.name = elementInstance.GetType().Name;

            return new MainToolbarElement() { VisualElement = elementInstance, Attribute = attribute, DecoratedType = type };
        }

        private static bool IsValidVisualElementType(Type type)
        {
            var visualElementType = typeof(VisualElement);

            return visualElementType != type &&
                visualElementType.IsAssignableFrom(type) &&
                !type.IsAbstract &&
                type.GetCustomAttribute<MainToolbarElementAttribute>() != null;
        }

        private static void OnProjectChange()
        {
            Refresh();
        }
    }
}