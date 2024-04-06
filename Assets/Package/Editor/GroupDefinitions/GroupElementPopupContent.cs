using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class GroupElementPopupContent : PopupWindowContent
    {
        private VisualElement[] _groupElements;
        private ScrollView _scrollView;
        private Vector2 _windowSize = new Vector2(50, 400);

        public GroupElementPopupContent(float width, VisualElement[] groupElements)
        {
            _groupElements = groupElements;
            _windowSize = new Vector2(_windowSize.x + width, _windowSize.y);
            _scrollView = new ScrollView(ScrollViewMode.Vertical);
            _scrollView.style.width = _windowSize.x;
            _scrollView.style.height = _windowSize.y;
            _scrollView.contentContainer.style.alignContent = Align.Center;
            _scrollView.contentContainer.style.alignItems = Align.Center;
            _scrollView.horizontalScrollerVisibility = ScrollerVisibility.Hidden;
        }

        public override Vector2 GetWindowSize()
        {
            return _windowSize;
        }

        public override void OnOpen()
        {
            foreach (var groupElement in _groupElements)
            {
                var container = CreateGroupElementContainer();
                container.Add(groupElement);
                groupElement.style.flexWrap = Wrap.Wrap;
                container.style.display = groupElement.style.display;
                _scrollView.Add(container);
            }

            editorWindow.rootVisualElement.Add(_scrollView);
        }

        private VisualElement CreateGroupElementContainer()
        {
            return new Box()
            {
                style =
                {
                    alignContent = Align.Center,
                    alignItems = Align.Center,
                    flexDirection = FlexDirection.Row,
                    flexGrow = 1,
                    minWidth = _windowSize.x,
                    minHeight = 30,
                    justifyContent = Justify.Center,
                    borderBottomColor = Color.black,
                    borderLeftColor = Color.black,
                    borderRightColor = Color.black,
                    borderTopColor = Color.black,
                    borderBottomWidth = 1,
                    borderLeftWidth = 1,
                    borderRightWidth = 1,
                    borderTopWidth = 1,
                    paddingTop = 3f,
                    paddingBottom = 3f
                }
            };
        }

        public override void OnGUI(Rect rect)
        {
            
        }
    }
}