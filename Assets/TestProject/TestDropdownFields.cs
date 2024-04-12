using Paps.UnityToolbarExtenderUIToolkit;
using System.Collections.Generic;
using UnityEngine.UIElements;

[MainToolbarElement]
public class TestDropdownField : DropdownField
{
    public TestDropdownField()
    {
        label = "Test DropdownField";
        choices = new List<string> { "Option 1", "Option 2" };
        SetValueWithoutNotify(choices[0]);
    }
}
