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

        private void OnEnable()
        {
            AssemblyReloadEvents.beforeAssemblyReload += Close;
        }

        private void OnDisable()
        {
            AssemblyReloadEvents.beforeAssemblyReload -= Close;
        }

        private void CreateGUI()
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

            var baseContainer = CreateBaseContainer();
            baseContainer.Add(_scrollView);

            rootVisualElement.Add(baseContainer);
        }

        private VisualElement CreateBaseContainer()
        {
            return new VisualElement()
            {
                style =
                {
                    flexGrow = 1,
                    borderBottomColor = Color.black,
                    borderTopColor = Color.black,
                    borderLeftColor = Color.black,
                    borderRightColor = Color.black,
                    borderBottomWidth = 2,
                    borderTopWidth = 2,
                    borderLeftWidth = 2,
                    borderRightWidth = 2,
                }
            };
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
                    width = Length.Percent(100),
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
