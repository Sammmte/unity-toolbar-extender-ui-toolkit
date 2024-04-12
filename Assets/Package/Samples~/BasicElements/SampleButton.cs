using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement(alignWhenSingle: ToolbarAlign.Left, order: 0)]
public class SampleButton : Button
{
    public SampleButton() // you can also construct your object with base()
    {
        text = "Sample Button";
        tooltip = "This is an example of a toolbar button";
        clicked += () => Debug.Log("A debug log from sample button");
    }
}
