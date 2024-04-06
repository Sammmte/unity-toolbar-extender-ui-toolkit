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
            schedule.Execute(() =>
            {
                if (childCount <= 1)
                    return;

                InitializeStyle(this[1]);
            }).Until(() => childCount > 1);
        }

        private void InitializeStyle(VisualElement dropdownElement)
        {
            labelElement.style.minWidth = 20;
            labelElement.style.paddingTop = 1;
            dropdownElement.AddToClassList("unity-toolbar-button");
            dropdownElement.style.maxWidth = 100;
        }
    }
}