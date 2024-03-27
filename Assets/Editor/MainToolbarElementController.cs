using UnityEngine;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class MainToolbarElementController : VisualElement
    {
        private const bool DEFAULT_VISIBILITY_VALUE = true;

        public string Id { get; }
        public VisualElement ControlledVisualElement { get; }
        private readonly IMainToolbarElementOverridesRepository _overridesRepository;
        private Image _buttonIconImage;

        public MainToolbarElementController(string id, VisualElement controlledVisualElement,
            IMainToolbarElementOverridesRepository overridesRepository)
        {
            Id = id;
            ControlledVisualElement = controlledVisualElement;
            _overridesRepository = overridesRepository;
        }

        public void Initialize()
        {
            _buttonIconImage = new Image();

            style.flexDirection = FlexDirection.Row;
            style.justifyContent = Justify.SpaceBetween;
            style.paddingLeft =
                style.paddingRight = 10;

            Add(CreateLabel());
            Add(CreateButton());
        }

        private bool VisibleValueOrDefault()
        {
            var possibleOverride = _overridesRepository.Get(Id);

            if (possibleOverride == null)
                return true;

            var overrideValue = possibleOverride.Value;

            return overrideValue.Visible;
        }

        private Label CreateLabel()
        {
            var label = new Label(GetName());

            label.style.alignSelf = Align.Center;

            return label;
        }

        private string GetName()
        {
            if (!string.IsNullOrEmpty(ControlledVisualElement.name))
                return ControlledVisualElement.name;

            return "Nameless element. Id: " + Id;
        }

        private Button CreateButton()
        {
            var button = new Button(ChangeVisibilityValue);

            UpdateIconImage(VisibleValueOrDefault());

            button.Add(_buttonIconImage);

            button.tooltip = "Change the visibility of this toolbar element";

            return button;
        }

        private Texture IconByVisibilityValue(bool visible)
        {
            if (visible)
                return Icons.VisibilityOnOverrideIcon;
            
            return Icons.VisibilityOffOverrideIcon;
        }

        private void ChangeVisibilityValue()
        {
            var currentOverrideValue = _overridesRepository.Get(Id);

            bool newValue;

            if (currentOverrideValue == null)
                newValue = !DEFAULT_VISIBILITY_VALUE;
            else
                newValue = !currentOverrideValue.Value.Visible;

            _overridesRepository.Save(new MainToolbarElementOverride(Id, newValue));
            UpdateIconImage(newValue);
        }

        private void UpdateIconImage(bool visible)
        {
            _buttonIconImage.image = IconByVisibilityValue(visible);
        }
    }
}