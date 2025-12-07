using Paps.UnityToolbarExtenderUIToolkit;
using System.Collections.Generic;
using UnityEngine.UIElements;

// Configured in Sample Group
[MainToolbarElement(id: "SampleDropdownField", order: 1)]
public class SampleDropdownField : DropdownField
{
    public void InitializeElement()
    {
        label = "Sample Dropdown";
        choices = new List<string>() { "Option 1", "Option 2" };
        SetValueWithoutNotify(choices[0]);
    }
}
