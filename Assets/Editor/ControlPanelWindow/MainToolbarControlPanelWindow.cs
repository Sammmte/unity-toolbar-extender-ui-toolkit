using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal struct OverridableElement
    {
        public string Id;
        public VisualElement VisualElement;
    }

    public class MainToolbarControlPanelWindow : EditorWindow
    {
        private MainToolbarElementController[] _controllers;

        public static void OpenWindow()
        {
            var window = GetWindow<MainToolbarControlPanelWindow>();

            window.titleContent = new GUIContent("Main Toolbar Control Panel");
        }

        private void CreateGUI()
        {
            MainToolbarAutomaticExtender.OnAddedCustomContainersToToolbar += Refresh;
            MainToolbarAutomaticExtender.OnRefresh += Refresh;

            if (ToolbarWrapper.IsAvailable)
                Refresh();
        }

        private void Refresh()
        {
            rootVisualElement.Clear();
            BuildGUI();
        }

        private void BuildGUI()
        {
            _controllers = CreateControllers();

            var containingBox = GetContainer();

            foreach (var controller in _controllers)
            {
                containingBox.Add(controller);
            }

            rootVisualElement.Add(containingBox);
        }

        private VisualElement GetContainer()
        {
            var scrollView = new ScrollView(ScrollViewMode.Vertical);

            scrollView.style.paddingBottom =
                scrollView.style.paddingTop =
                scrollView.style.paddingLeft =
                scrollView.style.paddingRight = 20;

            return scrollView;
        }

        private MainToolbarElementController[] CreateControllers()
        {
            return GetOverridableElements()
                .OrderBy(overridableElement => overridableElement.Id)
                .Select(overridableElement => new MainToolbarElementController(
                    overridableElement,
                    ServicesAndRepositories.MainToolbarElementOverridesRepository,
                    GetSubElementsIfAny(overridableElement.VisualElement))
                )
                .ToArray();
        }

        private OverridableElement[] GetSubElementsIfAny(VisualElement visualElement)
        {
            if(visualElement is GroupElement groupElement)
            {
                return groupElement.GroupedElements
                    .Select(el => new OverridableElement()
                    {
                        Id = MainToolbarElementOverrideIdProvider.IdOf(el),
                        VisualElement = el
                    })
                    .ToArray();
            }

            return new OverridableElement[0];
        }

        private OverridableElement[] GetOverridableElements()
        {
            return ToolbarWrapper.LeftContainer.Children()
                .Concat(ToolbarWrapper.RightContainer.Children())
                .Concat(ToolbarWrapper.PlayModeButtonsContainer.Children())
                .Concat(MainToolbarAutomaticExtender.CustomMainToolbarElements)
                .Concat(MainToolbarAutomaticExtender.GroupElements)
                .Select(visualElement => new OverridableElement() 
                { 
                    Id = MainToolbarElementOverrideIdProvider.IdOf(visualElement), 
                    VisualElement = visualElement 
                })
                .ToArray();
        }
    }
}