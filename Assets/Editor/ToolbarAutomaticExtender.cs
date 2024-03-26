using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    [InitializeOnLoad]
    public static class ToolbarAutomaticExtender
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

        private const int TOOLBAR_LEFT_CONTAINER_MIN_WIDTH_PERCENTAGE = 10;
        private const int TOOLBAR_RIGHT_CONTAINER_MIN_WIDTH_PERCENTAGE = 18;
        private const int TOOLBAR_PLAYMODE_CONTAINER_MIN_WIDTH_PERCENTAGE = 9;
        private const int CUSTOM_CONTAINER_MAX_WIDTH = 50;
        private const int SCROLL_VIEW_SCROLLER_HEIGHT = 4;
        private const int SCROLLER_LOW_BUTTON_HEIGHT = 7;
        private const int SCROLLER_HIGH_BUTTON_HEIGHT = 7;
        private const int SCROLLER_SLIDER_HEIGHT = 7;
        private const int SCROLL_VIEW_HORIZONTAL_PADDING = 4;

        public static VisualElement LeftCustomContainer { get; private set; } = CreateContainer("ToolbarAutomaticExtenderLeftContainer", FlexDirection.RowReverse);
        public static VisualElement RightCustomContainer { get; private set; } = CreateContainer("ToolbarAutomaticExtenderRightContainer", FlexDirection.Row);

        private static MainToolbarElement[] _mainToolbarElements;
        private static GroupDefinition[] _groupDefinitions;
        private static Dictionary<string, List<MainToolbarElement>> _elementsByGroup;

        static ToolbarAutomaticExtender()
        {
            Initialize();
        }

        private static void Initialize()
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

            ToolbarWrapper.OnNativeToolbarWrapped += () =>
            {
                ConfigureStyleOfContainers();

                ToolbarWrapper.CenterContainer.Insert(0, LeftCustomContainer);
                ToolbarWrapper.CenterContainer.Add(RightCustomContainer);

            };
        }

        private static void AddOrderedAlignedElementsToContainers(OrderedAlignedElement[] orderedAlignedElements)
        {
            var leftElements = orderedAlignedElements.Where(el => el.Alignment == ToolbarAlign.Left)
                .OrderBy(el => el.Order);

            var rightElements = orderedAlignedElements.Where(el => el.Alignment == ToolbarAlign.Right)
                .OrderBy(el => el.Order);

            foreach (var orderedAlignedElement in leftElements)
                LeftCustomContainer.Add(orderedAlignedElement.VisualElement);

            foreach (var orderedAlignedElement in rightElements)
                RightCustomContainer.Add(orderedAlignedElement.VisualElement);
        }

        private static Group[] GetGroups()
        {
            var groups = new List<Group>();

            foreach (var groupDefinition in _groupDefinitions)
            {
                var elementsOfThisGroup = _elementsByGroup[groupDefinition.Name];

                if (elementsOfThisGroup.Count == 0)
                    continue;

                groups.Add(new Group()
                {
                    Name = groupDefinition.Name,
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
                elementsByGroup.Add(groupDefinition.Name, new List<MainToolbarElement>());
            }

            foreach (var element in _mainToolbarElements)
            {
                var containingGroupDefinition = _groupDefinitions
                    .Where(groupDefinition => groupDefinition.ToolbarElementsTypes.
                        Contains(element.DecoratedType.FullName))
                    .FirstOrDefault();

                if (containingGroupDefinition != null)
                    elementsByGroup[containingGroupDefinition.Name].Add(element);
            }

            return elementsByGroup;
        }

        private static GroupDefinition[] LoadGroupDefinitions()
        {
            var paths = AssetDatabase.FindAssets("t:" + nameof(GroupDefinition))
                .Select(guid => AssetDatabase.GUIDToAssetPath(guid));

            return paths
                .Select(path => AssetDatabase.LoadAssetAtPath<GroupDefinition>(path))
                .Where(groupDefinition => !string.IsNullOrEmpty(groupDefinition.name))
                .GroupBy(groupDefinition => groupDefinition.Name)
                .Select(group => group.First())
                .ToArray();
        } 

        private static OrderedAlignedElement[] GetOrderedAlignedElements(Group[] groups, MainToolbarElement[] singles)
        {
            var orderedAlignedElements = new List<OrderedAlignedElement>();

            foreach(var group in groups)
            {
                orderedAlignedElements.Add(
                    new OrderedAlignedElement()
                    {
                        Alignment = group.Alignment,
                        Order = group.Order,
                        VisualElement = new GroupElement(group.Name, group.MainToolbarElements.Select(m => m.VisualElement).ToArray())
                    });
            }

            foreach(var single in singles)
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
            ToolbarWrapper.LeftContainer.style.minWidth = Length.Percent(TOOLBAR_LEFT_CONTAINER_MIN_WIDTH_PERCENTAGE);
            ToolbarWrapper.LeftContainer.style.justifyContent = Justify.FlexStart;

            ToolbarWrapper.RightContainer.style.minWidth = Length.Percent(TOOLBAR_RIGHT_CONTAINER_MIN_WIDTH_PERCENTAGE);
            ToolbarWrapper.RightContainer.style.justifyContent = Justify.FlexStart;

            ToolbarWrapper.PlayModeButtonsContainer.style.minWidth = Length.Percent(TOOLBAR_PLAYMODE_CONTAINER_MIN_WIDTH_PERCENTAGE);
            ToolbarWrapper.PlayModeButtonsContainer.style.justifyContent = Justify.Center;

            ToolbarWrapper.CenterContainer.style.flexGrow = 1;
            ToolbarWrapper.CenterContainer.style.justifyContent = Justify.Center;

            ToolbarWrapper.CenterContainer.parent.style.justifyContent = Justify.SpaceBetween;
        }

        private static MainToolbarElement[] GetMainToolbarElements()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes());

            return FilterRawElements(types)
                .Concat(GetElementsFromProviders(types))
                .ToArray();
        }

        private static IEnumerable<MainToolbarElement> FilterRawElements(IEnumerable<Type> allTypes)
        {
            return allTypes
                .Where(type => IsValidVisualElementType(type))
                .Select(type =>
                {
                    var elementInstance = (VisualElement)Activator.CreateInstance(type);
                    var attribute = type.GetCustomAttribute<MainToolbarElementAttribute>();

                    return new MainToolbarElement() { VisualElement = elementInstance, Attribute = attribute, DecoratedType = type };
                });
        }

        private static IEnumerable<MainToolbarElement> GetElementsFromProviders(IEnumerable<Type> allTypes)
        {
            return allTypes
                .Where(type => IsValidElementProviderType(type))
                    .Select(type =>
                    {
                        var providerInstance = (IMainToolbarElementProvider)Activator.CreateInstance(type);
                        var attribute = type.GetCustomAttribute<MainToolbarElementAttribute>();
                        var isGrouped = _groupDefinitions.Any(groupDefinition => groupDefinition.ToolbarElementsTypes
                                .Contains(type.FullName));
                        var element = providerInstance.GetElement(isGrouped);

                        return new MainToolbarElement() { VisualElement = element, Attribute = attribute, DecoratedType = type };
                    }
                );
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

        private static VisualElement CreateContainer(string name, FlexDirection flexDirection)
        {
            var container = new VisualElement()
            {
                name = name,
                style = {
                    flexGrow = 1,
                    flexDirection = flexDirection,
                    alignItems = Align.Center,
                    alignContent = Align.Center,
                    maxWidth = Length.Percent(CUSTOM_CONTAINER_MAX_WIDTH)
                }
            };

            ScrollView scrollView = CreateSrollContainer();

            container.Add(scrollView);

            return scrollView;
        }

        private static ScrollView CreateSrollContainer()
        {
            var scrollView = new ScrollView(ScrollViewMode.Horizontal);

            scrollView.style.paddingLeft = SCROLL_VIEW_HORIZONTAL_PADDING;
            scrollView.style.paddingRight = SCROLL_VIEW_HORIZONTAL_PADDING;

            scrollView.verticalScrollerVisibility = ScrollerVisibility.Hidden;

            var scroller = scrollView.horizontalScroller;

            var leftButton = scroller.lowButton;
            var rightButton = scroller.highButton;
            var slider = scroller.slider;

            scroller.style.height = SCROLL_VIEW_SCROLLER_HEIGHT;
            leftButton.style.height = SCROLLER_LOW_BUTTON_HEIGHT;
            rightButton.style.height = SCROLLER_HIGH_BUTTON_HEIGHT;
            slider.style.height = SCROLLER_SLIDER_HEIGHT;

            return scrollView;
        }
    }
}