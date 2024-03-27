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
        private class Group
        {
            public string Name;
            public ToolbarAlign Alignment;
            public int Order;
            public MainToolbarElement[] MainToolbarElements;
        }

        private class OrderedAlignedElement
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
        private static Dictionary<string, List<MainToolbarElement>> _elementsByGroup = new Dictionary<string, List<MainToolbarElement>>();
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
            var nativeElements = GetNativeElements();
            SetNativeElementsDefaultState(nativeElements);
            ApplyOverridesOnNativeElements(nativeElements);
            OnRefresh?.Invoke();
        }

        private static void ResetCustomContainers()
        {
            LeftCustomContainer.ClearContainer();
            RightCustomContainer.ClearContainer();
        }

        private static void BuildCustomToolbarContainers()
        {
            _groupDefinitions = LoadGroupDefinitions();
            _mainToolbarElements = GetMainToolbarElements();

            if (_mainToolbarElements.Count() == 0)
                return;

            _elementsByGroup = CacheElementsByGroup();

            var groups = GetGroups();
            var singles = GetSingles();

            _groupElements = GetGroupElements(groups);

            var orderedAlignedElements = GetOrderedAlignedElements(groups, singles);

            ApplyOverridesOnCustomElements(orderedAlignedElements);

            AddOrderedAlignedElementsToContainers(orderedAlignedElements);
        }

        private static void ApplyOverridesOnCustomElements(OrderedAlignedElement[] defaultElements)
        {
            foreach(var element in defaultElements)
            {
                ApplyOverride(element.VisualElement);
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

        private static GroupElement[] GetGroupElements(Group[] groups)
        {
            return groups.Select(group => new GroupElement(group.Name, group.MainToolbarElements.Select(m => m.VisualElement).ToArray()))
                .ToArray();
        }

        private static void ApplyFixedChangesToToolbar()
        {
            ConfigureStyleOfContainers();
            AddCustomContainers();
            var nativeElements = GetNativeElements();
            SetNativeElementsDefaultState(nativeElements);
            ApplyOverridesOnNativeElements(nativeElements);
        }

        private static void AddCustomContainers()
        {
            ToolbarWrapper.CenterContainer.Insert(0, LeftCustomContainer);
            ToolbarWrapper.CenterContainer.Add(RightCustomContainer);
        }

        private static void AddOrderedAlignedElementsToContainers(OrderedAlignedElement[] orderedAlignedElements)
        {
            var leftElements = orderedAlignedElements.Where(el => el.Alignment == ToolbarAlign.Left)
                .OrderBy(el => el.Order);

            var rightElements = orderedAlignedElements.Where(el => el.Alignment == ToolbarAlign.Right)
                .OrderBy(el => el.Order);

            foreach (var orderedAlignedElement in leftElements)
                LeftCustomContainer.AddToContainer(orderedAlignedElement.VisualElement);

            foreach (var orderedAlignedElement in rightElements)
                RightCustomContainer.AddToContainer(orderedAlignedElement.VisualElement);
        }

        private static Group[] GetGroups()
        {
            var groups = new List<Group>();

            foreach (var groupDefinition in _groupDefinitions)
            {
                var elementsOfThisGroup = _elementsByGroup[groupDefinition.GroupName];

                if (elementsOfThisGroup.Count == 0)
                    continue;

                groups.Add(new Group()
                {
                    Name = groupDefinition.GroupName,
                    Alignment = groupDefinition.Alignment,
                    Order = groupDefinition.Order,
                    MainToolbarElements = elementsOfThisGroup.ToArray()
                });
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

        private static Dictionary<string, List<MainToolbarElement>> CacheElementsByGroup()
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

        private static OrderedAlignedElement[] GetOrderedAlignedElements(Group[] groups, MainToolbarElement[] singles)
        {
            var orderedAlignedElements = new List<OrderedAlignedElement>();

            foreach (var group in groups)
            {
                orderedAlignedElements.Add(
                    new OrderedAlignedElement()
                    {
                        Alignment = group.Alignment,
                        Order = group.Order,
                        VisualElement = _groupElements.First(el => el.GroupName == group.Name)
                    });
            }

            foreach (var single in singles)
            {
                orderedAlignedElements.Add(
                    new OrderedAlignedElement()
                    {
                        Alignment = single.Attribute.AlignWhenSingle,
                        Order = single.Attribute.Order,
                        VisualElement = single.VisualElement
                    });
            }

            return orderedAlignedElements.ToArray();
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
                .Where(type => IsValidVisualElementType(type) || IsValidElementProviderType(type))
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
            if(type == typeof(VisualElement))
            {
                var elementInstance = (VisualElement)Activator.CreateInstance(type);
                var attribute = type.GetCustomAttribute<MainToolbarElementAttribute>();

                if (string.IsNullOrEmpty(elementInstance.name))
                    elementInstance.name = elementInstance.GetType().Name;

                return new MainToolbarElement() { VisualElement = elementInstance, Attribute = attribute, DecoratedType = type };
            }
            else
            {
                var providerInstance = (IMainToolbarElementProvider)Activator.CreateInstance(type);
                var attribute = type.GetCustomAttribute<MainToolbarElementAttribute>();
                var isGrouped = _groupDefinitions.Any(groupDefinition => groupDefinition.ToolbarElementsTypes
                        .Contains(type.FullName));
                var elementInstance = providerInstance.CreateElement(isGrouped);

                if (string.IsNullOrEmpty(elementInstance.name))
                    elementInstance.name = providerInstance.GetType().Name;

                return new MainToolbarElement() { VisualElement = elementInstance, Attribute = attribute, DecoratedType = type };
            }
        }

        private static bool IsValidVisualElementType(Type type)
        {
            var visualElementType = typeof(VisualElement);

            return visualElementType != type &&
                visualElementType.IsAssignableFrom(type) &&
                !type.IsAbstract &&
                type.GetCustomAttribute<MainToolbarElementAttribute>() != null;
        }

        private static bool IsValidElementProviderType(Type type)
        {
            var elementProviderType = typeof(IMainToolbarElementProvider);

            return elementProviderType != type &&
                elementProviderType.IsAssignableFrom(type) &&
                !type.IsAbstract &&
                type.GetCustomAttribute<MainToolbarElementAttribute>() != null;
        }

        private static void OnProjectChange()
        {
            Refresh();
        }
    }
}