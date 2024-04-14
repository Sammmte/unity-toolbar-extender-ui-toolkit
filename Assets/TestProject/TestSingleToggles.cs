using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine.UIElements;

[MainToolbarElement(nameof(TestSingleToggle))]
public class TestSingleToggle : Toggle
{
    public TestSingleToggle() : base("Toggle 1")
    {

    }
}

[MainToolbarElement(nameof(TestSingleToggle2))]
public class TestSingleToggle2 : Toggle
{
    public TestSingleToggle2() : base("Toggle 2")
    {

    }
}

[MainToolbarElement(nameof(TestSingleToggle3))]
public class TestSingleToggle3 : Toggle
{
    public TestSingleToggle3() : base("Toggle 3")
    {

    }
}