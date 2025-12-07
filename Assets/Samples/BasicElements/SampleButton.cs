using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement(id: "SampleButton", order: 0)]
public class SampleButton : Button
{
    public void InitializeElement()
    {
        text = "Sample Button";
        tooltip = "This is an example of a toolbar button";
        clicked += () => Debug.Log("A debug log from sample button");
    }
}
