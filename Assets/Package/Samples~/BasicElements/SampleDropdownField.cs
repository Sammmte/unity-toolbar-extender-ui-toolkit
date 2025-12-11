using System.Collections.Generic;
using UnityEditor.Toolbars;
using UnityEngine.UIElements;

// Configured in Sample Group
[Paps.UnityToolbarExtenderUIToolkit.MainToolbarElement(id: "SampleDropdownField", order: 1)]
public class SampleDropdownField : DropdownField
{
    [UnityEditor.Toolbars.MainToolbarElement("SampleDropdownField", defaultDockPosition = MainToolbarDockPosition.Right)]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummyGroup()
    {
        // Return null here
        return null;
    }
    
    public void InitializeElement()
    {
        label = "Sample Dropdown";
        choices = new List<string>() { "Option 1", "Option 2" };
        SetValueWithoutNotify(choices[0]);
    }
}
