using UnityEditor.Toolbars;
using UnityEditor.UIElements;

[Paps.UnityToolbarExtenderUIToolkit.MainToolbarElement("SampleColorField")]
public class SampleColorField : ColorField
{
    [UnityEditor.Toolbars.MainToolbarElement("SampleColorField", defaultDockPosition = MainToolbarDockPosition.Left)]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummyGroup()
    {
        // Return null here
        return null;
    }
    
    public void InitializeElement()
    {
        label = "Sample Color";
    }
}
