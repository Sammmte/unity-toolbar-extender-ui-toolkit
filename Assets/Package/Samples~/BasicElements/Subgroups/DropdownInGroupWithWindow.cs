using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement(nameof(DropdownInGroupWithWindow))]
public class DropdownInGroupWithWindow : EditorToolbarDropdown
{
    public DropdownInGroupWithWindow()
    {
        text = nameof(DropdownInGroupWithWindow);
        clicked += ShowDropdownWindow;
    }

    private void ShowDropdownWindow()
    {
        var newWindowDropdown = ScriptableObject.CreateInstance<DropdownWindow>();
        newWindowDropdown.ShowAsDropdownForMainToolbar(worldBound, new Vector2(200, 200));
    }
}

[GroupPopupSubWindow]
public class DropdownWindow : EditorWindow
{
    private void CreateGUI()
    {
        var debugButton = new Button(() => Debug.Log("What a debug!"));
        debugButton.text = "Debug";

        var closeButton = new Button(() => Close());
        closeButton.text = "Close";

        rootVisualElement.Add(debugButton);
        rootVisualElement.Add(closeButton);
    }
}
