using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class ByAttributeMainToolbarElementRepository : IMainToolbarElementRepository
    {
        public MainToolbarElement[] GetAll()
        {
            return TypeCache.GetTypesWithAttribute<MainToolbarElementAttribute>()
                .Where(type => IsValidVisualElementType(type))
                .Select(type => GetMainToolbarElementFromType(type))
                .ToArray();
        }

        private MainToolbarElement GetMainToolbarElementFromType(Type type)
        {
            var elementInstance = (VisualElement)Activator.CreateInstance(type);
            var attribute = type.GetCustomAttribute<MainToolbarElementAttribute>();

            if (string.IsNullOrEmpty(elementInstance.name))
                elementInstance.name = elementInstance.GetType().Name;

            return new MainToolbarElement(attribute.Id, elementInstance, attribute.Alignment, 
                attribute.Order, attribute.UseRecommendedStyles);
        }

        private bool IsValidVisualElementType(Type type)
        {
            var visualElementType = typeof(VisualElement);

            return visualElementType != type &&
                visualElementType.IsAssignableFrom(type) &&
                !type.IsAbstract;
        }
    }
}