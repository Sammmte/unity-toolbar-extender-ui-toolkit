using System.Linq;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public class MainToolbarCustomContainer : VisualElement
    {
        private const int SCROLL_VIEW_SCROLLER_HEIGHT = 2;
        private const int SCROLLER_LOW_BUTTON_HEIGHT = 6;
        private const int SCROLLER_HIGH_BUTTON_HEIGHT = 6;
        private const int SCROLLER_SLIDER_HEIGHT = 6;
        private const int SCROLL_VIEW_HORIZONTAL_PADDING = 5;
        private const int SCROLL_VIEW_SCROLLER_BORDER_TOP_WIDTH = 0;

        private ScrollView _scrollView;

        public MainToolbarCustomContainer(string name, FlexDirection flexDirection)
        {
            this.name = name;

            style.flexDirection = flexDirection;
            style.flexGrow = 1;
            style.width = 0;

            _scrollView = CreateScrollView();
            Add(_scrollView);
        }

        private ScrollView CreateScrollView()
        {
            var scrollView = new ScrollView(ScrollViewMode.Horizontal);

            scrollView.style.paddingLeft = SCROLL_VIEW_HORIZONTAL_PADDING;
            scrollView.style.paddingRight = SCROLL_VIEW_HORIZONTAL_PADDING;

            scrollView.verticalScrollerVisibility = ScrollerVisibility.Hidden;

            var scroller = scrollView.horizontalScroller;

            var leftButton = scroller.lowButton;
            var rightButton = scroller.highButton;
            var slider = scroller.slider;

            scroller.style.height = SCROLL_VIEW_SCROLLER_HEIGHT;
            scroller.style.borderTopWidth = SCROLL_VIEW_SCROLLER_BORDER_TOP_WIDTH;
            leftButton.style.height = SCROLLER_LOW_BUTTON_HEIGHT;
            rightButton.style.height = SCROLLER_HIGH_BUTTON_HEIGHT;
            slider.style.height = SCROLLER_SLIDER_HEIGHT;

            return scrollView;
        }

        public void AddToContainer(VisualElement child)
        {
            _scrollView.Add(child);
        }

        public void ClearContainer()
        {
            _scrollView.Clear();
        }

        public VisualElement[] GetContainerChilds()
        {
            return _scrollView.Children().ToArray();
        }
    }
}