using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class LayerFieldRecommendedStyle : RecommendedStyle
    {
        private readonly LayerField _layerField;

        public LayerFieldRecommendedStyle(LayerField layerField) 
        {
            _layerField = layerField;
        }

        protected override void ApplyRootElementStyle() 
        {
            _layerField.labelElement.style.minWidth = Length.Auto();

            var inputFieldIndex = 1;

            if (string.IsNullOrEmpty(_layerField.label))
                inputFieldIndex = 0;

            var inputElement = _layerField[inputFieldIndex];
            inputElement.style.overflow = Overflow.Visible;
        }
    }
}