using UnityEngine;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class MainToolbarElementController : VisualElement
    {
        public string Id { get; }
        public VisualElement ControlledVisualElement { get; }
        private readonly IMainToolbarElementOverridesRepository _overridesRepository;
        private Label _label;
        private Button _button;
        private Image _buttonIconImage;

        private StyleColor _defaultButtonColor;

        public MainToolbarElementController(string id, VisualElement controlledVisualElement,
            IMainToolbarElementOverridesRepository overridesRepository)
        {
            Id = id;
            ControlledVisualElement = controlledVisualElement;
            _overridesRepository = overridesRepository;

            _buttonIconImage = new Image();

            style.flexDirection = FlexDirection.Row;
            style.justifyContent = Justify.SpaceBetween;
            style.paddingLeft =
                style.paddingRight = 10;

            _label = CreateLabel();
            _button = CreateButton();

            _defaultButtonColor = _button.style.backgroundColor;

            UpdateButtonStatus(VisibleValueOrDefault());

            Add(_label);
            Add(_button);
        }

        private bool VisibleValueOrDefault()
        {
            var possibleOverride = _overridesRepository.Get(Id);

            if (possibleOverride == null)
                return CurrentDisplayValueAsBool();

            var overrideValue = possibleOverride.Value;

            return overrideValue.Visible;
        }

        private Label CreateLabel()
        {
            var label = new Label(Id);

            label.style.alignSelf = Align.Center;

            return label;
        }

        private Button CreateButton()
        {
            var button = new Button(ChangeVisibilityValue);

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
                newValue = !CurrentDisplayValueAsBool();
            else
                newValue = !currentOverrideValue.Value.Visible;

            _overridesRepository.Save(new MainToolbarElementOverride(Id, newValue));
            UpdateButtonStatus(newValue);

            MainToolbarAutomaticExtender.Refresh();
        }

        private bool CurrentDisplayValueAsBool()
        {
            if (ControlledVisualElement.style.display == DisplayStyle.None)
                return false;
            else
                return true;
        }

        private void UpdateButtonStatus(bool visible)
        {
            _buttonIconImage.image = IconByVisibilityValue(visible);
            _button.style.backgroundColor = GetButtonColor(visible);
        }

        private StyleColor GetButtonColor(bool visible)
        {
            if (visible)
                return _defaultButtonColor;
            else
                return new StyleColor(Color.black);
        }
    }
}