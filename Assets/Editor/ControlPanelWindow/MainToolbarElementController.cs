using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class MainToolbarElementController : VisualElement
    {
        private const float LEFT_PADDING_SINGLE = 19;
        private const float RIGHT_PADDING = 10;

        private readonly IMainToolbarElementOverridesRepository _overridesRepository;
        private Label _label;
        private Button _button;
        private Image _buttonIconImage;
        private Foldout _foldout;

        private StyleColor _defaultButtonColor;

        public string Id { get; }
        public VisualElement ControlledVisualElement { get; }
        public bool HoldsAGroup => _foldout != null;
        public bool HoldsANativeElement { get; private set; }

        public MainToolbarElementController(OverridableElement overridableElement,
            IMainToolbarElementOverridesRepository overridesRepository, params OverridableElement[] subElements)
        {
            Id = overridableElement.Id;
            ControlledVisualElement = overridableElement.VisualElement;
            HoldsANativeElement = overridableElement.IsNative;
            name = Id + "-Controller";
            _overridesRepository = overridesRepository;

            _buttonIconImage = new Image();

            style.flexDirection = FlexDirection.Row;
            style.justifyContent = Justify.SpaceBetween;
            style.paddingRight = RIGHT_PADDING;

            _label = CreateLabel();
            _button = CreateButton();

            _defaultButtonColor = _button.style.backgroundColor;

            UpdateButtonStatus(VisibleValueOrDefault());
            BuildAsGroupOrSingle(subElements);
        }

        private void BuildAsGroupOrSingle(OverridableElement[] subElements)
        {
            if (subElements.Length > 0)
                BuildFoldout(subElements);
            else
                BuildSingleElement();
        }

        private void BuildSingleElement()
        {
            style.paddingLeft = LEFT_PADDING_SINGLE;

            Add(_label);
            Add(_button);
        }

        private void BuildFoldout(OverridableElement[] subElements)
        {
            _foldout = new Foldout() { text = _label.text };
            

            foreach (var overridable in subElements)
            {
                var subController = new MainToolbarElementController(overridable, _overridesRepository);

                _foldout.Add(subController);

                _foldout.value = false;
            }

            Add(_foldout);
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

            button.style.alignSelf = Align.FlexStart;

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