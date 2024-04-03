using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public class MainToolbarDropdownField : DropdownField
    {
        public MainToolbarDropdownField()
        {
            Initialize();
        }

        public MainToolbarDropdownField(string label, List<string> choices, int defaultIndex) : base(
            label, choices, defaultIndex)
        {
            Initialize();
        }

        private void Initialize()
        {
            var labelElement = this[0];
            var dropdownElement = this[1];

            labelElement.style.minWidth = 20;
            labelElement.style.paddingTop = 1;
            dropdownElement.AddToClassList("unity-toolbar-button");
            dropdownElement.style.maxWidth = 100;
        }
    }
}