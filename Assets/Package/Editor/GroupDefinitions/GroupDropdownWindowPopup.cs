using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class GroupDropdownWindowPopup : EditorWindow
    {
        private VisualElement[] _groupElements;
        private ScrollView _scrollView;

        public void Initialize(VisualElement[] groupElements)
        {
            _groupElements = groupElements;
            hideFlags = HideFlags.DontSave;
        }

        public void CreateGUI()
        {
            _scrollView = new ScrollView(ScrollViewMode.Vertical);
            _scrollView.contentContainer.style.alignContent = Align.Center;
            _scrollView.contentContainer.style.alignItems = Align.Center;
            _scrollView.horizontalScrollerVisibility = ScrollerVisibility.Hidden;

            foreach (var groupElement in _groupElements)
            {
                var container = CreateGroupElementContainer();
                container.Add(groupElement);
                groupElement.style.flexWrap = Wrap.Wrap;
                container.style.display = groupElement.style.display;
                _scrollView.Add(container);
            }

            rootVisualElement.Add(_scrollView);
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
                    width = Length.Auto(),
                    height = Length.Auto(),
                    justifyContent = Justify.Center,
                    borderTopColor = Color.black,
                    borderTopWidth = 1,
                    paddingTop = 3f,
                    paddingBottom = 3f,
                    paddingLeft = 8,
                    paddingRight = 8,
                }
            };
        }
    }
}
