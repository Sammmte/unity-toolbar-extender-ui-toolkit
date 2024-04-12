﻿using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class ByAttributeMainToolbarElementRepository : IMainToolbarElementRepository
    {
        private MainToolbarElement[] _elementsInProject;

        public MainToolbarElement[] GetAll()
        {
            if (_elementsInProject == null)
                _elementsInProject = LoadMainToolbarElements();

            return _elementsInProject.ToArray();
        }

        private MainToolbarElement[] LoadMainToolbarElements()
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

            return new MainToolbarElement(elementInstance, attribute.AlignWhenSingle, 
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