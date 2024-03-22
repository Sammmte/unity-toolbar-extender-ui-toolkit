using Paps.UnityToolbarExtenderUIToolkit;
using System.Collections.Generic;

[MainToolbarElement(align: ToolbarAlign.Right, group: TestGroups.RightGroup)]
public class TestRightGroupDropdown : MainToolbarDropdown
{
    public TestRightGroupDropdown() : base("1234", new List<string>() { "Option 1", "Option 2" }, 0)
    {

    }
}