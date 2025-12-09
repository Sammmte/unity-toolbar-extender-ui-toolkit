using UnityEditor.Toolbars;
using UnityEngine.UIElements;

[Paps.UnityToolbarExtenderUIToolkit.MainToolbarElement("SampleTextField")]
public class SampleTextField : TextField
{
    [UnityEditor.Toolbars.MainToolbarElement("SampleTextField", defaultDockPosition = MainToolbarDockPosition.Left)]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummyGroup()
    {
        // Return null here
        return null;
    }
    
    public void InitializeElement()
    {
        label = "Sample Text";
    }
}