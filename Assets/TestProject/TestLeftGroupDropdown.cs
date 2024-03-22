using Paps.UnityToolbarExtenderUIToolkit;
using System.Collections.Generic;

[MainToolbarElement(group: TestGroups.LeftGroup)]
public class TestLeftGroupDropdown : MainToolbarDropdown
{
    public TestLeftGroupDropdown() : base("1234", new List<string>() { "Option 1", "Option 2" }, 0)
    {

    }
}