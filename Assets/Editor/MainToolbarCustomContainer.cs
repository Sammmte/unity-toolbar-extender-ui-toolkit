using System.Linq;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class MainToolbarCustomContainer : VisualElement
    {
        private const float SCROLL_VIEW_SCROLLER_HEIGHT = 1;
        private const float SCROLLER_HEIGHT = 5;
        private const float SCROLL_VIEW_HORIZONTAL_PADDING = 5;
        private const float SCROLL_VIEW_SCROLLER_BORDER_TOP_WIDTH = 0;

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
            leftButton.style.height = SCROLLER_HEIGHT;
            rightButton.style.height = SCROLLER_HEIGHT;
            slider.style.height = SCROLLER_HEIGHT;

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