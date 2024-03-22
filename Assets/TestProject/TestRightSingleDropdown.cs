using Paps.UnityToolbarExtenderUIToolkit;
using System.Collections.Generic;

[MainToolbarElement(align: ToolbarAlign.Right)]
public class TestRightSingleDropdown : MainToolbarDropdown
{
    public TestRightSingleDropdown() : base("1234", new List<string>() { "Option 1", "Option 2" }, 0)
    {
        
    }
}
