using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;

[Paps.UnityToolbarExtenderUIToolkit.MainToolbarElement("SampleEnumField")]
public class SampleEnumField : EnumField
{
    [UnityEditor.Toolbars.MainToolbarElement("SampleEnumField", defaultDockPosition = MainToolbarDockPosition.Right)]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummyGroup()
    {
        // Return null here
        return null;
    }
    
    public void InitializeElement()
    {
        label = "Sample Enum";
        Init(KeyCode.A);
    }
}
