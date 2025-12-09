using System;
using UnityEditor.Toolbars;
using UnityEditor.UIElements;

[Paps.UnityToolbarExtenderUIToolkit.MainToolbarElement("SampleEnumFlagsField")]
public class SampleEnumFlagsField : EnumFlagsField
{
    [UnityEditor.Toolbars.MainToolbarElement("SampleEnumFlagsField", defaultDockPosition = MainToolbarDockPosition.Left)]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummyGroup()
    {
        // Return null here
        return null;
    }
    
    public void InitializeElement()
    {
        label = "Sample Enum Flags";
        Init(MovementStates.None);
    }
}

[Flags]
public enum MovementStates
{
    None = 0,
    Idle = 1,
    Sitting = 2,
    Walking = 4,
    Running = 8,
    Crouching = 16,
    Crawling = 32
}