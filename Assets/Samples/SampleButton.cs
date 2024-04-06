using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor.Toolbars;
using UnityEngine;

[MainToolbarElement(alignWhenSingle: ToolbarAlign.Left, order: 0)]
public class SampleButton : EditorToolbarButton
{
    public SampleButton() // you can also construct your object with base()
    {
        text = "Sample Button";
        tooltip = "This is an example of a toolbar button with an icon";
        clicked += () => Debug.Log("A debug log from sample button with icon");
    }
}
