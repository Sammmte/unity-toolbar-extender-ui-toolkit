using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public class MainToolbarControlPanelWindow : EditorWindow
    {
        private MainToolbarElementController[] _controllers;

        [MenuItem(ToolInfo.EDITOR_MENU_BASE + "/Main Toolbar Control Panel")]
        public static void OpenWindow()
        {
            var window = GetWindow<MainToolbarControlPanelWindow>();

            window.titleContent = new GUIContent("Main Toolbar Control Panel");
        }

        private void CreateGUI()
        {
            _controllers = GetEligibleElements();

            var containingBox = GetContainingBox();

            foreach(var controller in _controllers)
            {
                controller.Initialize();
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

        private MainToolbarElementController[] GetEligibleElements()
        {
            return ToolbarWrapper.LeftContainer.Children()
                .Concat(ToolbarWrapper.RightContainer.Children())
                .Concat(ToolbarWrapper.PlayModeButtonsContainer.Children())
                .Select(visualElement => new MainToolbarElementController(
                    IdFor(visualElement), 
                    visualElement, 
                    ServicesAndRepositories.MainToolbarElementOverridesRepository)
                )
                .Where(controller => controller.Id != null)
                .GroupBy(controller => controller.Id)
                .Select(group => group.First())
                .ToArray();
        }

        private string IdFor(VisualElement visualElement)
        {
            if (HasFixedId(visualElement, out string id))
                return id;

            var idByName = string.IsNullOrEmpty(visualElement.name) ? null : visualElement.name;

            return idByName;
        }

        private bool HasFixedId(VisualElement visualElement, out string id)
        {
            id = UnityNativeElementsIds.IdOf(visualElement);

            return id != null;
        }
    }
}