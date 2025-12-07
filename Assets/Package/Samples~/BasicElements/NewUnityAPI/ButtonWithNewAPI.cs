using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;

[Paps.UnityToolbarExtenderUIToolkit.MainToolbarElement("ButtonWithNewAPI")]
public class ButtonWithNewAPI : Button
{
    [UnityEditor.Toolbars.MainToolbarElement("ButtonWithNewAPI", defaultDockPosition = MainToolbarDockPosition.Middle)]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummy()
    {
        // Return null here
        return null;
    }

    public void InitializeElement()
    {
        Debug.Log("Initializing Button With New API");
        
        text = "This my Paps Button!";
        clicked += () =>
        {
            Debug.Log("My Paps button works!");
        };
    }
}