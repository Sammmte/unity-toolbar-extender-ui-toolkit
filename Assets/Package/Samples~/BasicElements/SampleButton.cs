using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;

[Paps.UnityToolbarExtenderUIToolkit.MainToolbarElement(id: "SampleButton", order: 0)]
public class SampleButton : Button
{
    [UnityEditor.Toolbars.MainToolbarElement("SampleButton", defaultDockPosition = MainToolbarDockPosition.Middle)]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummyGroup()
    {
        // Return null here
        return null;
    }
    
    public void InitializeElement()
    {
        text = "Sample Button";
        tooltip = "This is an example of a toolbar button";
        clicked += () => Debug.Log("A debug log from sample button");
    }
}
