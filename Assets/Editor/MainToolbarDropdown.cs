using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public class MainToolbarDropdown : DropdownField
    {
        public MainToolbarDropdown(string label)
        : base(label)
        {
            InitializeStyle();
        }

        public MainToolbarDropdown(List<string> choices, string defaultValue, Func<string, string> formatSelectedValueCallback = null, Func<string, string> formatListItemCallback = null)
            : this(null, choices, defaultValue, formatSelectedValueCallback, formatListItemCallback)
        {
        }

        public MainToolbarDropdown(string label, List<string> choices, string defaultValue, Func<string, string> formatSelectedValueCallback = null, Func<string, string> formatListItemCallback = null)
            : base(label, choices, defaultValue, formatSelectedValueCallback, formatListItemCallback)
        {
            InitializeStyle();
        }

        public MainToolbarDropdown(List<string> choices, int defaultIndex, Func<string, string> formatSelectedValueCallback = null, Func<string, string> formatListItemCallback = null)
            : this(null, choices, defaultIndex, formatSelectedValueCallback, formatListItemCallback)
        {
        }

        public MainToolbarDropdown(string label, List<string> choices, int defaultIndex, Func<string, string> formatSelectedValueCallback = null, Func<string, string> formatListItemCallback = null)
            : base(label, choices, defaultIndex, formatSelectedValueCallback, formatListItemCallback)
        {
            InitializeStyle();
        }

        private void InitializeStyle()
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