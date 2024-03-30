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

        private OrganizationalFoldableContainer _nativeElementsContainer;
        private OrganizationalFoldableContainer _singleElementsContainer;
        private OrganizationalFoldableContainer _groupElementsContainer;
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

            BuildFixedGUI();

            if (ToolbarWrapper.IsAvailable)
                Refresh();
        }

        private void OnDestroy()
        {
            MainToolbarAutomaticExtender.OnAddedCustomContainersToToolbar -= Refresh;
            MainToolbarAutomaticExtender.OnRefresh -= Refresh;
        }

        private void Refresh()
        {
            BuildDynamicGUI();
        }

        private void BuildFixedGUI()
        {
            var windowContainer = GetContainer();

            _singleElementsContainer = new OrganizationalFoldableContainer(
                    SINGLE_ELEMENTS_CONTAINER_NAME, SINGLE_ELEMENTS_FOLDOUT_TEXT);
            _groupElementsContainer = new OrganizationalFoldableContainer(
                    GROUP_ELEMENTS_CONTAINER_NAME, GROUP_ELEMENTS_FOLDOUT_TEXT);
            _nativeElementsContainer = new OrganizationalFoldableContainer(
                    NATIVE_ELEMENTS_CONTAINER_NAME, NATIVE_ELEMENTS_FOLDOUT_TEXT);

            windowContainer.Add(_singleElementsContainer);
            windowContainer.Add(_groupElementsContainer);
            windowContainer.Add(_nativeElementsContainer);

            rootVisualElement.Add(windowContainer);
        }

        private void BuildDynamicGUI()
        {
            _controllers = CreateControllers();

            var controllersOfNativeElements = _controllers
                .Where(controller => controller.HoldsANativeElement);
            var controllersOfGroups = _controllers
                .Where(controller => controller.HoldsAGroup);
            var controllersOfSingleElements = _controllers
                .Where(controller => !controller.HoldsAGroup && !controller.HoldsANativeElement)
                .Where(controller => SingleControllerIsNotInsideAGroupController(controller, controllersOfGroups));

            _singleElementsContainer.SetControllers(controllersOfSingleElements);
            _groupElementsContainer.SetControllers(controllersOfGroups);
            _nativeElementsContainer.SetControllers(controllersOfNativeElements);
        }

        private static bool SingleControllerIsNotInsideAGroupController(MainToolbarElementController controller, IEnumerable<MainToolbarElementController> controllersOfGroups)
        {
            foreach (var groupController in controllersOfGroups)
                if (groupController.ContainsSubController(controller.Id))
                    return false;

            return true;
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
            var nativeElements = MainToolbarAutomaticExtender.NativeElements
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
            string containerName, string foldoutText)
        {
            return new OrganizationalFoldableContainer(containerName, foldoutText);
        }
    }
}