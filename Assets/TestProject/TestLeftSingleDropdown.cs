using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine.UIElements;
using System.Collections.Generic;

[MainToolbarElement()]
public class TestLeftSingleDropdown : MainToolbarDropdown
{
    public TestLeftSingleDropdown() : base("1234", new List<string>() { "Option 1", "Option 2" }, 0)
    {
        
    }
}
