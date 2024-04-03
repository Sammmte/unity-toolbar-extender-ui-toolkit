using Paps.UnityToolbarExtenderUIToolkit;
using System.Collections.Generic;

[MainToolbarElement]
public class TestGroupDropdown : MainToolbarDropdownField
{
    public TestGroupDropdown() : base(
        "Test 1", 
        new List<string>() { "Option 1", "Option 2"}, 
        0)
    {

    }
}

[MainToolbarElement]
public class TestGroupDropdown2 : MainToolbarDropdownField
{
    public TestGroupDropdown2() : base(
        "Test 2",
        new List<string>() { "Option 1", "Option 2" },
        0)
    {

    }
}

[MainToolbarElement]
public class TestGroupDropdown3 : MainToolbarDropdownField
{
    public TestGroupDropdown3() : base(
        "Test 3",
        new List<string>() { "Option 1", "Option 2" },
        0)
    {

    }
}