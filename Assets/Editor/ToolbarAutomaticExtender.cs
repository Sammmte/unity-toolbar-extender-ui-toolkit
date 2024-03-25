using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    [InitializeOnLoad]
    public static class ToolbarAutomaticExtender
    {
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

        static ToolbarAutomaticExtender()
        {
            Initialize();
        }

        private static void Initialize()
        {
            var elementsWithAttributes = GetMainToolbarElements();

            if (elementsWithAttributes.Count() == 0)
                return;

            AddToolbarElementsToContainers(elementsWithAttributes);

            ToolbarWrapper.OnNativeToolbarWrapped += () =>
            {
                ConfigureStyleOfContainers();

                ToolbarWrapper.CenterContainer.Insert(0, LeftCustomContainer);
                ToolbarWrapper.CenterContainer.Add(RightCustomContainer);
            };
        }

        private static void AddToolbarElementsToContainers((VisualElement Element, MainToolbarElementAttribute Attribute)[] elementsWithAttributes)
        {
            var leftElements = elementsWithAttributes.Where(tuple => tuple.Attribute.Align == ToolbarAlign.Left)
                .OrderBy(tuple => tuple.Attribute.Order);
            var rightElements = elementsWithAttributes.Where(tuple => tuple.Attribute.Align == ToolbarAlign.Right)
                .OrderBy(tuple => tuple.Attribute.Order);

            var leftGroups = leftElements.GroupBy(tuple => tuple.Attribute.Group);
            var rightGroups = rightElements.GroupBy(tuple => tuple.Attribute.Group);

            AddSingleElementOrGroupElement(leftGroups, LeftCustomContainer);
            AddSingleElementOrGroupElement(rightGroups, RightCustomContainer);
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

        private static void AddSingleElementOrGroupElement(IEnumerable<IGrouping<string, (VisualElement Element, MainToolbarElementAttribute Attribute)>> groups, VisualElement container)
        {
            foreach (var group in groups)
            {
                if (string.IsNullOrEmpty(group.Key))
                {
                    foreach (var elementWithAttribute in group)
                    {
                        container.Add(elementWithAttribute.Element);
                    }
                }
                else
                {
                    var groupElement = new GroupElement(group.Key,
                        group.ToArray().Select(tuple => tuple.Element).ToArray());

                    container.Add(groupElement);
                }
            }
        }

        private static (VisualElement Element, MainToolbarElementAttribute Attribute)[] GetMainToolbarElements()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes());

            return FilterRawElements(types)
                .Concat(GetElementsFromProviders(types))
                .ToArray();
        }

        private static IEnumerable<(VisualElement Element, MainToolbarElementAttribute Attribute)> FilterRawElements(IEnumerable<Type> allTypes)
        {
            return allTypes
                .Where(type => IsValidVisualElementType(type))
                .Select(type =>
                {
                    var elementInstance = (VisualElement)Activator.CreateInstance(type);
                    var attribute = type.GetCustomAttribute<MainToolbarElementAttribute>();

                    return (elementInstance, attribute);
                });
        }

        private static IEnumerable<(VisualElement Element, MainToolbarElementAttribute Attribute)> GetElementsFromProviders(IEnumerable<Type> allTypes)
        {
            return allTypes
                .Where(type => IsValidElementProviderType(type))
                    .Select(type =>
                    {
                        var providerInstance = (IMainToolbarElementProvider)Activator.CreateInstance(type);
                        var attribute = type.GetCustomAttribute<MainToolbarElementAttribute>();
                        var element = providerInstance.GetElement(!string.IsNullOrEmpty(attribute.Group));

                        return (element, attribute);
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