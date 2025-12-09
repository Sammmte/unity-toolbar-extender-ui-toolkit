using UnityEditor.Toolbars;
using UnityEngine.UIElements;

[Paps.UnityToolbarExtenderUIToolkit.MainToolbarElement("SampleVector3Field")]
public class SampleVector3Field : Vector3Field
{
    [UnityEditor.Toolbars.MainToolbarElement("SampleVector3Field", defaultDockPosition = MainToolbarDockPosition.Left)]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummyGroup()
    {
        // Return null here
        return null;
    }
    
    public void InitializeElement()
    {
        label = "Sample Vector 3";
    }
}