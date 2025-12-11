using UnityEditor.Toolbars;
using UnityEditor.UIElements;

[Paps.UnityToolbarExtenderUIToolkit.MainToolbarElement("SampleTagField")]
public class SampleTagField : TagField
{
    [UnityEditor.Toolbars.MainToolbarElement("SampleTagField", defaultDockPosition = MainToolbarDockPosition.Left)]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummyGroup()
    {
        // Return null here
        return null;
    }
    
    public void InitializeElement()
    {
        label = "Sample Tag";
    }
}