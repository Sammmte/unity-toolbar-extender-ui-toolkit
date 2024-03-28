using Paps.UnityToolbarExtenderUIToolkit;
using System.Collections.Generic;

[MainToolbarElement]
public class TestSingleDropdown : MainToolbarDropdown
{
    public TestSingleDropdown() : base(
        nameof(TestSingleDropdown), 
        new List<string>() { "Option 1", "Option 2"}, 
        0)
    {

    }
}

[MainToolbarElement]
public class TestSingleDropdown2 : MainToolbarDropdown
{
    public TestSingleDropdown2() : base(
        nameof(TestSingleDropdown2),
        new List<string>() { "Option 1", "Option 2" },
        0)
    {

    }
}

[MainToolbarElement]
public class TestSingleDropdown3 : MainToolbarDropdown
{
    public TestSingleDropdown3() : base(
        nameof(TestSingleDropdown3),
        new List<string>() { "Option 1", "Option 2" },
        0)
    {

    }
}