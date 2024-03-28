using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public class MainToolbarToggle : Toggle
    {
        public MainToolbarToggle()
        {
            Initialize();
        }

        public MainToolbarToggle(string label) : base(label)
        {
            Initialize();
        }

        private void Initialize()
        {
            var labelElement = this[0];

            labelElement.style.minWidth = 20;
        }
    }
}