using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine.UIElements;
using System.Collections.Generic;

[MainToolbarElement(group: "Left Test Group")]
public class TestDropdown : DropdownField
{
    public TestDropdown() : base("Test Dropdown", new List<string>() { "Option 1", "Option 2" }, 0)
    {

    }
}