using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public class MainToolbarDropdownProvider : SimpleMainToolbarElementProvider
    {
        private string _label;
        private List<string> _choices;
        private int _defaultIndex;

        public MainToolbarDropdownProvider(string label, List<string> choices, int defaultIndex)
        {
            _label = label;
            _choices = choices;
            _defaultIndex = defaultIndex;
        }

        protected override VisualElement BuildElement(bool isGrouped)
        {
            var dropdownField = new DropdownField(_label, _choices, _defaultIndex);
            InitializeStyle(dropdownField);

            return dropdownField;
        }

        private void InitializeStyle(DropdownField dropdownField)
        {
            var labelElement = dropdownField[0];
            var dropdownElement = dropdownField[1];

            labelElement.style.minWidth = 20;
            labelElement.style.paddingTop = 1;
            dropdownElement.AddToClassList("unity-toolbar-button");
            dropdownElement.style.maxWidth = 100;
        }
    }
}