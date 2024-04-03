using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public class MainToolbarToggleField : Toggle
    {
        public MainToolbarToggleField()
        {
            Initialize();
        }

        public MainToolbarToggleField(string label) : base(label)
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