using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public class MainToolbarControlPanelWindow : EditorWindow
    {
        private const string NATIVE_ELEMENTS_CONTAINER_NAME = "UnityNativeElementsContainer";
        private const string NATIVE_ELEMENTS_FOLDOUT_TEXT = "Unity Native Elements";

        private const string SINGLE_ELEMENTS_CONTAINER_NAME = "SingleElementsContainer";
        private const string SINGLE_ELEMENTS_FOLDOUT_TEXT = "Single Elements";

        private const string GROUP_ELEMENTS_CONTAINER_NAME = "GroupElementsContainer";
        private const string GROUP_ELEMENTS_FOLDOUT_TEXT = "Groups";

        private const float MAIN_CONTAINER_PADDING_TOP = 5;

        private static readonly Color ORGANIZATIONAL_FOLDABLE_CONTAINER_BORDER_COLOR = new Color(153f / 255f, 153f / 255f, 153f / 255f);

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

            var windowContainer = GetContainer();

            var controllersOfNativeElements = _controllers
                .Where(controller => controller.HoldsANativeElement);
            var controllersOfGroups = _controllers
                .Where(controller => controller.HoldsAGroup);
            var controllersOfSingleElements = _controllers
                .Where(controller => !controller.HoldsAGroup && !controller.HoldsANativeElement);

            var foldableContainers = new VisualElement[]
            {
                CreateOrganizationalFoldableContainer(
                    SINGLE_ELEMENTS_CONTAINER_NAME, SINGLE_ELEMENTS_FOLDOUT_TEXT, controllersOfSingleElements),
                CreateOrganizationalFoldableContainer(
                    GROUP_ELEMENTS_CONTAINER_NAME, GROUP_ELEMENTS_FOLDOUT_TEXT, controllersOfGroups),
                CreateOrganizationalFoldableContainer(
                    NATIVE_ELEMENTS_CONTAINER_NAME, NATIVE_ELEMENTS_FOLDOUT_TEXT, controllersOfNativeElements)
            };

            foreach (var controllersContainer in foldableContainers)
            {
                windowContainer.Add(controllersContainer);
            }

            rootVisualElement.Add(windowContainer);
        }

        private VisualElement GetContainer()
        {
            var scrollView = new ScrollView(ScrollViewMode.Vertical);

            scrollView.style.paddingTop = MAIN_CONTAINER_PADDING_TOP;

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
                    .Select(el => new OverridableElement(
                        MainToolbarElementOverrideIdProvider.IdOf(el),
                        el,
                        false)
                    )
                    .ToArray();
            }

            return new OverridableElement[0];
        }

        private OverridableElement[] GetOverridableElements()
        {
            var nativeElements = ToolbarWrapper.LeftContainer.Children()
                .Concat(ToolbarWrapper.RightContainer.Children())
                .Concat(ToolbarWrapper.PlayModeButtonsContainer.Children())
                .Select(visualElement => new OverridableElement(
                    MainToolbarElementOverrideIdProvider.IdOf(visualElement),
                    visualElement,
                    true
                    )
                );

            var customElements = MainToolbarAutomaticExtender.CustomMainToolbarElements
                .Concat(MainToolbarAutomaticExtender.GroupElements)
                .Select(visualElement => new OverridableElement(
                    MainToolbarElementOverrideIdProvider.IdOf(visualElement),
                    visualElement,
                    false
                    )
                );

            return nativeElements.Concat(customElements)
                .ToArray();
        }

        private VisualElement CreateOrganizationalFoldableContainer(
            string containerName, string foldoutText, 
            IEnumerable<MainToolbarElementController> controllers)
        {
            var box = new Box() { name = containerName };
            box.style.borderTopColor = ORGANIZATIONAL_FOLDABLE_CONTAINER_BORDER_COLOR;
            box.style.borderTopWidth = 1;

            var foldout = new Foldout() { text = foldoutText };
            foldout.value = false;

            foreach(var controller in controllers)
            {
                foldout.Add(controller);
            }

            box.Add(foldout);

            return box;
        }
    }
}