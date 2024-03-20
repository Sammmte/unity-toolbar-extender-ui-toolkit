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
        public static VisualElement LeftCustomContainer { get; private set; } = CreateContainer(FlexDirection.RowReverse);
        public static VisualElement RightCustomContainer { get; private set; } = CreateContainer(FlexDirection.Row);

        static ToolbarAutomaticExtender()
        {
            var elementsWithAttributes = GetMainToolbarElements();

            var leftElements = elementsWithAttributes.Where(tuple => tuple.Item2.Align == ToolbarAlign.Left);
            var rightElements = elementsWithAttributes.Where(tuple => tuple.Item2.Align == ToolbarAlign.Right);

            var leftGroups = leftElements.GroupBy(tuple => tuple.Item2.Group);
            var rightGroups = rightElements.GroupBy(tuple => tuple.Item2.Group);
            AddSingleElementOrGroupElement(leftGroups, LeftCustomContainer);
            AddSingleElementOrGroupElement(rightGroups, RightCustomContainer);

            ToolbarWrapper.ExecuteIfOrWhenIsInitialized(() =>
            {
                ToolbarWrapper.CenterContainer.Insert(0, LeftCustomContainer);
                ToolbarWrapper.CenterContainer.Add(RightCustomContainer);
            });
        }

        private static void AddSingleElementOrGroupElement(IEnumerable<IGrouping<string, (VisualElement, MainToolbarElementAttribute)>> groups, VisualElement container)
        {
            foreach (var group in groups)
            {
                if (string.IsNullOrEmpty(group.Key))
                {
                    foreach (var elementWithAttribute in group)
                    {
                        container.Add(elementWithAttribute.Item1);
                    }
                }
                else
                {
                    var groupElement = new GroupElement(group.Key,
                        group.ToArray().Select(tuple => tuple.Item1).ToArray());

                    container.Add(groupElement);
                }
            }
        }

        private static (VisualElement, MainToolbarElementAttribute)[] GetMainToolbarElements()
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

        private static VisualElement CreateContainer(FlexDirection flexDirection)
        {
            var container = new VisualElement()
            {
                style = {
                flexGrow = 1,
                flexDirection = flexDirection,
                alignItems = Align.Center,
                alignContent = Align.Center
                }
            };

            return container;
        }
    }
}