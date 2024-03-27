﻿using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public class MainToolbarControlPanelWindow : EditorWindow
    {
        private struct OverridableElement
        {
            public string Id;
            public VisualElement VisualElement;
        }

        private MainToolbarElementController[] _controllers;

        [MenuItem(ToolInfo.EDITOR_MENU_BASE + "/Main Toolbar Control Panel")]
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

            var containingBox = GetContainingBox();

            foreach (var controller in _controllers)
            {
                containingBox.Add(controller);
            }

            rootVisualElement.Add(containingBox);
        }

        private Box GetContainingBox()
        {
            var box = new Box();

            box.style.paddingBottom = 
                box.style.paddingTop = 
                box.style.paddingLeft = 
                box.style.paddingRight = 20;

            return box;
        }

        private MainToolbarElementController[] CreateControllers()
        {
            return GetOverridableElements()
                .OrderBy(overridableElement => overridableElement.Id)
                .Select(overridableElement => new MainToolbarElementController(
                    overridableElement.Id,
                    overridableElement.VisualElement, 
                    ServicesAndRepositories.MainToolbarElementOverridesRepository)
                )
                .ToArray();
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