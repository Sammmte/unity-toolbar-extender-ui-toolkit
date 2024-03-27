using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public class MainToolbarToggleProvider : SimpleMainToolbarElementProvider
    {
        private string _label;

        public MainToolbarToggleProvider(string label)
        {
            _label = label;
        }

        protected override VisualElement BuildElement(bool isGrouped)
        {
            var toggle = new Toggle(_label);
            InitializeStyle(toggle);

            return toggle;
        }

        private void InitializeStyle(Toggle toggle)
        {
            var labelElement = toggle[0];

            labelElement.style.minWidth = 20;
        }
    }
}