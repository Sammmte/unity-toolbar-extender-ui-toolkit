using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor.Toolbars;
using UnityEngine;

[MainToolbarElement] // No need to specify values in attribute
public class SampleButtonInsideGroup : EditorToolbarButton
{
    public SampleButtonInsideGroup() // you can also construct your object with base()
    {
        text = "Group Button";
        tooltip = "This is an example of a toolbar button inside a group";
        clicked += () => Debug.Log("A debug log from sample button inside a group");
    }
}