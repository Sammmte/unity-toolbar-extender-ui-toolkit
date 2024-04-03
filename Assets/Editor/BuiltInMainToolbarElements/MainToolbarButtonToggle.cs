using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public class MainToolbarButtonToggle : EditorToolbarToggle
    {
        private const float TEXT_HORIZONTAL_PADDING = 2;

        new public Texture2D icon
        {
            get
            {
                return base.icon;
            }
            set
            {
                base.icon = value;
                UpdateIconState();
            }
        }

        new public Texture2D onIcon
        {
            get
            {
                return base.onIcon;
            }
            set
            {
                base.onIcon = value;
                UpdateIconState();
            }
        }

        new public Texture2D offIcon
        {
            get
            {
                return base.offIcon;
            }
            set
            {
                base.offIcon = value;
                UpdateIconState();
            }
        }

        private VisualElement _textElement;
        private VisualElement _iconContainer;

        public MainToolbarButtonToggle(string text, Texture2D iconOn, Texture2D iconOff) : base(text, iconOn, iconOff)
        {
            _textElement = this[0];
            _iconContainer = this[1];

            _textElement.style.paddingLeft =
                _textElement.style.paddingRight = TEXT_HORIZONTAL_PADDING;

            UpdateIconState();
        }

        public MainToolbarButtonToggle() : this(string.Empty, null, null)
        {

        }

        public MainToolbarButtonToggle(string text) : this(text, null, null)
        {

        }

        public MainToolbarButtonToggle(Texture2D icon) : this(string.Empty, icon, icon)
        {

        }

        public MainToolbarButtonToggle(string text, Texture2D icon) : this(text, icon, icon)
        {

        }

        public MainToolbarButtonToggle(Texture2D iconOn, Texture2D iconOff) : this(string.Empty, iconOn, iconOff)
        {

        }

        private void UpdateIconState()
        {
            if (icon == null)
                _iconContainer.style.display = DisplayStyle.None;
            else
                _iconContainer.style.display = DisplayStyle.Flex;
        }
    }
}