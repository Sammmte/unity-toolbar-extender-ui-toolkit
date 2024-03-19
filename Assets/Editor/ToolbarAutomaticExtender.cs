using System;
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

            foreach (var elementWithAttribute in leftElements)
                LeftCustomContainer.Add(elementWithAttribute.Item1);

            foreach (var elementWithAttribute in rightElements)
                RightCustomContainer.Add(elementWithAttribute.Item1);

            ToolbarWrapper.ExecuteIfOrWhenIsInitialized(() =>
            {
                ToolbarWrapper.CenterContainer.Insert(0, LeftCustomContainer);
                ToolbarWrapper.CenterContainer.Add(RightCustomContainer);
            });
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