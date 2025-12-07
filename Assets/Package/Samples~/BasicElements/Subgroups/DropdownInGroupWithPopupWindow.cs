using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;
using MainToolbarElement = Paps.UnityToolbarExtenderUIToolkit.MainToolbarElementAttribute;

[MainToolbarElement(nameof(DropdownInGroupWithPopupWindow))]
public class DropdownInGroupWithPopupWindow : EditorToolbarDropdown
{
    public DropdownInGroupWithPopupWindow()
    {
        text = nameof(DropdownInGroupWithPopupWindow);
        clicked += ShowPopupWindow;
    }

    private void ShowPopupWindow()
    {
        UnityEditor.PopupWindow.Show(worldBound, new PopupWindowContentThatWorksInASubgroup());
    }
}

[GroupPopupSubWindow]
public class PopupWindowContentThatWorksInASubgroup : PopupWindowContent
{
    public override void OnOpen()
    {
        var debugButton = new Button(() => Debug.Log("What a debug!"));
        debugButton.text = "Debug";

        var closeButton = new Button(() => editorWindow.Close());
        closeButton.text = "Close";

        editorWindow.rootVisualElement.Add(debugButton);
        editorWindow.rootVisualElement.Add(closeButton);
    }

    public override void OnGUI(Rect rect)
    {
        
    }
}
