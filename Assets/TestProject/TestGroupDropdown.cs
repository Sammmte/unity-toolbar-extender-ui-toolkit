using Paps.UnityToolbarExtenderUIToolkit;
using System.Collections.Generic;

[MainToolbarElement(group: "Left Test Group")]
public class TestGroupDropdown : MainToolbarDropdown
{
    public TestGroupDropdown() : base("1234", new List<string>() { "Option 1", "Option 2" }, 0)
    {

    }
}