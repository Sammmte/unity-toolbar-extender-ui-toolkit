using Paps.UnityToolbarExtenderUIToolkit;
using System.Collections.Generic;
using UnityEngine.UIElements;

// Configured in Sample Group
[MainToolbarElement(alignWhenSingle: ToolbarAlign.Left, order: 1)]
public class SampleDropdownField : DropdownField
{
    public SampleDropdownField() // you can also construct your object with base()
    {
        label = "Sample Dropdown";
        choices = new List<string>() { "Option 1", "Option 2" };
        SetValueWithoutNotify(choices[0]);
    }
}
