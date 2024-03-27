using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement]
public class TestVisualElementAndElementProvider : Toggle, IMainToolbarElementProvider
{
    public TestVisualElementAndElementProvider() : base("But i'm a toggle")
    {

    }

    public VisualElement CreateElement(bool isGrouped)
    {
        if (isGrouped)
        {
            var button = new Button(() => Debug.Log("EXITO"));
            button.text = "I should be a button";

            return button;
        }
        else
        {
            return new EditorToolbarButton("I should be a button", () => Debug.Log("EXITO"));
        }
    }
}
