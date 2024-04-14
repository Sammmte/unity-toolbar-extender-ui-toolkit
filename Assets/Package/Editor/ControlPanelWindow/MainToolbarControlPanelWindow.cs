using System;
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

        private Button _resetOverridesButton;

        private VisualElement _noElementsMessageElement;
        private VisualElement _windowContainer;

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
            SetDefaultView();

            if (MainToolbar.IsAvailable)
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
            SetCorrespondingView();
        }

        private void BuildFixedGUI()
        {
            _windowContainer = GetContainer();

            _resetOverridesButton = new Button(GlobalActions.ResetOverridesIfUserAccepts);
            _resetOverridesButton.text = "Reset Overrides";

            _singleElementsContainer = new OrganizationalFoldableContainer(
                    SINGLE_ELEMENTS_CONTAINER_NAME, SINGLE_ELEMENTS_FOLDOUT_TEXT);
            _groupElementsContainer = new OrganizationalFoldableContainer(
                    GROUP_ELEMENTS_CONTAINER_NAME, GROUP_ELEMENTS_FOLDOUT_TEXT);
            _nativeElementsContainer = new OrganizationalFoldableContainer(
                    NATIVE_ELEMENTS_CONTAINER_NAME, NATIVE_ELEMENTS_FOLDOUT_TEXT);

            _noElementsMessageElement = CreateNoElementsMessageElement();

            rootVisualElement.Add(_resetOverridesButton);
            rootVisualElement.Add(_windowContainer);
        }

        private void SetDefaultView()
        {
            _windowContainer.Add(_noElementsMessageElement);
        }

        private void SetCorrespondingView()
        {
            if(_controllers.Length > 0)
            {
                if(_windowContainer.Contains(_noElementsMessageElement))
                    _windowContainer.Remove(_noElementsMessageElement);

                if (!_windowContainer.Contains(_singleElementsContainer))
                {
                    _windowContainer.Add(_singleElementsContainer);
                    _windowContainer.Add(_groupElementsContainer);
                    _windowContainer.Add(_nativeElementsContainer);
                }
            }
            else
            {
                if (_windowContainer.Contains(_singleElementsContainer))
                {
                    _windowContainer.Remove(_singleElementsContainer);
                    _windowContainer.Remove(_groupElementsContainer);
                    _windowContainer.Remove(_nativeElementsContainer);
                }
                
                if (!_windowContainer.Contains(_noElementsMessageElement))
                    _windowContainer.Add(_noElementsMessageElement);
            }
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
                    GetSubElementsIfAny(overridableElement))
                )
                .ToArray();
        }

        private OverridableElement[] GetSubElementsIfAny(OverridableElement overridableElement)
        {
            if(overridableElement.VisualElement is GroupElement)
            {
                return MainToolbarAutomaticExtender.GetElementsOfGroup(overridableElement.Id)
                    .Select(el => new OverridableElement(
                        el.Id,
                        el.VisualElement,
                        false)
                    )
                    .ToArray();
            }

            return new OverridableElement[0];
        }

        private OverridableElement[] GetOverridableElements()
        {
            var nativeElements = MainToolbarAutomaticExtender.NativeElements
                .Select(nativeElement => new OverridableElement(
                    nativeElement.Id,
                    nativeElement.VisualElement,
                    true
                    )
                );

            var customElements = MainToolbarAutomaticExtender.CustomMainToolbarElements
                .Concat(MainToolbarAutomaticExtender.GroupElements)
                .Select(mainToolbarElement => new OverridableElement(
                    mainToolbarElement.Id,
                    mainToolbarElement.VisualElement,
                    false
                    )
                );

            return nativeElements.Concat(customElements)
                .ToArray();
        }

        private VisualElement CreateNoElementsMessageElement()
        {
            return new HelpBox("No main toolbar elements were found. To use the automatic toolbar extender define a main toolbar element. See the docs.", HelpBoxMessageType.Warning);
        }
    }
}