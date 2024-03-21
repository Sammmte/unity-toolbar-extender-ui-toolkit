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
        public static VisualElement LeftCustomContainer { get; private set; } = CreateContainer("ToolbarAutomaticExtenderLeftContainer", FlexDirection.RowReverse);
        public static VisualElement RightCustomContainer { get; private set; } = CreateContainer("ToolbarAutomaticExtenderRightContainer", FlexDirection.Row);

        static ToolbarAutomaticExtender()
        {
            var elementsWithAttributes = GetMainToolbarElements();

            var leftElements = elementsWithAttributes.Where(tuple => tuple.Attribute.Align == ToolbarAlign.Left);
            var rightElements = elementsWithAttributes.Where(tuple => tuple.Attribute.Align == ToolbarAlign.Right);

            var leftGroups = leftElements.GroupBy(tuple => tuple.Attribute.Group);
            var rightGroups = rightElements.GroupBy(tuple => tuple.Attribute.Group);
            AddSingleElementOrGroupElement(leftGroups, LeftCustomContainer);
            AddSingleElementOrGroupElement(rightGroups, RightCustomContainer);

            ToolbarWrapper.OnNativeToolbarWrapped += () =>
            {
                ToolbarWrapper.CenterContainer.Insert(0, LeftCustomContainer);
                ToolbarWrapper.CenterContainer.Add(RightCustomContainer);
            };
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
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => IsValidVisualElementType(type))
                .Select(type =>
                {
                    var elementInstance = (VisualElement)Activator.CreateInstance(type);
                    var attribute = type.GetCustomAttribute<MainToolbarElementAttribute>();

                    return (elementInstance, attribute);
                })
                .ToArray();
        }

        private static bool IsValidVisualElementType(Type type)
        {
            var visualElementType = typeof(VisualElement);

            return visualElementType != type &&
                visualElementType.IsAssignableFrom(type) &&
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
                }
            };

            return container;
        }
    }
}