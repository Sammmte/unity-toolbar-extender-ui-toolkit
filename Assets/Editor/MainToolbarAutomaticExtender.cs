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

        private static MainToolbarAutomaticExtenderOptionsButton _optionsButton = CreateOptionsButton();

        internal static Type[] MainToolbarElementTypesInProject { get; private set; } = new Type[0];
        private static MainToolbarElement[] _mainToolbarElements = new MainToolbarElement[0];
        private static GroupDefinition[] _groupDefinitions = new GroupDefinition[0];
        private static Dictionary<string, List<MainToolbarElement>> _elementsByGroup = new Dictionary<string, List<MainToolbarElement>>();

        public static MainToolbarCustomContainer LeftCustomContainer { get; private set; } = new MainToolbarCustomContainer("ToolbarAutomaticExtenderLeftContainer", FlexDirection.RowReverse);
        public static MainToolbarCustomContainer RightCustomContainer { get; private set; } = new MainToolbarCustomContainer("ToolbarAutomaticExtenderRightContainer", FlexDirection.Row);

        public static VisualElement[] CustomMainToolbarElements => _mainToolbarElements.Select(m => m.VisualElement).ToArray();

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

            var orderedAlignedElements = GetOrderedAlignedElements(groups, singles);

            AddOrderedAlignedElementsToContainers(orderedAlignedElements);
        }

        private static void ApplyFixedChangesToToolbar()
        {
            ConfigureStyleOfContainers();
            AddCustomContainers();
            AddOptionsButton();
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
                var groupElement = new GroupElement(group.Name, group.MainToolbarElements.Select(m => m.VisualElement).ToArray());

                orderedAlignedElements.Add(
                    new OrderedAlignedElement()
                    {
                        Alignment = group.Alignment,
                        Order = group.Order,
                        VisualElement = groupElement
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

        private static void AddOptionsButton()
        {
            ToolbarWrapper.RightContainer.Insert(0, _optionsButton);
        }

        private static MainToolbarAutomaticExtenderOptionsButton CreateOptionsButton()
        {
            return new MainToolbarAutomaticExtenderOptionsButton(
                new MainToolbarAutomaticExtenderOptionsButton.Option()
                {
                    Text = "Refresh",
                    Action = Refresh
                }
                );
        }
    }
}