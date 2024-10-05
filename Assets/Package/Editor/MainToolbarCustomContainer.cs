using System.Linq;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class MainToolbarCustomContainer : VisualElement
    {
        private const string LAST_SCROLLER_POSITION_SAVE_KEY_BASE = "main-toolbar-custom-container:last-scroller-position:";

        private const float SCROLL_VIEW_SCROLLER_HEIGHT = 1;
        private const float SCROLLER_HEIGHT = 5;
        private const float SCROLL_VIEW_HORIZONTAL_PADDING = 5;
        private const float SCROLL_VIEW_SCROLLER_BORDER_TOP_WIDTH = 0;

        private string _id;
        private ScrollView _scrollView;
        private Scroller _scroller;
        private VisualElement _container;

        public MainToolbarCustomContainer(string id, FlexDirection flexDirection)
        {
            _id = id;
            name = id;

            style.flexDirection = flexDirection;
            style.flexGrow = 1;
            style.width = 0;

            _container = CreateAndAddContainer(flexDirection);
        }

        private VisualElement CreateAndAddContainer(FlexDirection flexDirection)
        {
            _scrollView = new ScrollView(ScrollViewMode.Horizontal);

            _scrollView.style.paddingLeft = SCROLL_VIEW_HORIZONTAL_PADDING;
            _scrollView.style.paddingRight = SCROLL_VIEW_HORIZONTAL_PADDING;

            _scrollView.verticalScrollerVisibility = ScrollerVisibility.Hidden;
            _scrollView.contentContainer.style.flexDirection = flexDirection;

            _scroller = _scrollView.horizontalScroller;

            var leftButton = _scroller.lowButton;
            var rightButton = _scroller.highButton;
            var slider = _scroller.slider;

            _scroller.style.height = SCROLL_VIEW_SCROLLER_HEIGHT;
            _scroller.style.borderTopWidth = SCROLL_VIEW_SCROLLER_BORDER_TOP_WIDTH;
            leftButton.style.height = SCROLLER_HEIGHT;
            rightButton.style.height = SCROLLER_HEIGHT;
            slider.style.height = SCROLLER_HEIGHT;

            Add(_scrollView);
            _scrollView.RegisterCallback<GeometryChangedEvent>(LoadLastScrollerPosition);

            return _scrollView;
        }

        private void LoadLastScrollerPosition(GeometryChangedEvent eventArgs)
        {
            _scroller.value = (float)LastScrollerPosition();
            _scroller.valueChanged += SaveScrollerPosition;

            _scrollView.UnregisterCallback<GeometryChangedEvent>(LoadLastScrollerPosition);
        }

        private double LastScrollerPosition()
        {
            return JsonEditorPrefs.ToolDataRepository.GetDouble(GetFullKey(), 0d);
        }

        private void SaveScrollerPosition(float newPosition)
        {
            JsonEditorPrefs.ToolDataRepository.SetDouble(GetFullKey(), newPosition);
        }

        private string GetFullKey() => LAST_SCROLLER_POSITION_SAVE_KEY_BASE + _id;

        public void AddToContainer(VisualElement child)
        {
            _container.Add(child);
        }

        public void ClearContainer()
        {
            _container.Clear();
        }

        public VisualElement[] GetContainerChilds()
        {
            return _container.Children().ToArray();
        }
    }
}