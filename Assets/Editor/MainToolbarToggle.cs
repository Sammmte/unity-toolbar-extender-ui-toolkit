using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public class MainToolbarToggle : Toggle
    {
        public MainToolbarToggle(string label) : base(label)
        {
            InitializeStyle();
        }

        private void InitializeStyle()
        {
            var labelElement = this[0];

            labelElement.style.minWidth = 20;
        }
    }
}