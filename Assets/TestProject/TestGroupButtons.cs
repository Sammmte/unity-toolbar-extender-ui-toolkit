using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;

[MainToolbarElement]
public class TestGroupButton : MainToolbarButtonProvider
{
    public TestGroupButton() : base(nameof(TestGroupButton), () => Debug.Log("This is " + nameof(TestGroupButton)))
    {
    }
}

[MainToolbarElement]
public class TestGroupButton2 : MainToolbarButtonProvider
{
    public TestGroupButton2() : base(nameof(TestGroupButton2), () => Debug.Log("This is " + nameof(TestGroupButton2)))
    {
    }
}

[MainToolbarElement]
public class TestGroupButton3 : MainToolbarButtonProvider
{
    public TestGroupButton3() : base(nameof(TestGroupButton3), () => Debug.Log("This is " + nameof(TestGroupButton3)))
    {
    }
}

[MainToolbarElement]
public class TestGroupButton4 : MainToolbarButtonProvider
{
    public TestGroupButton4() : base(nameof(TestGroupButton4), () => Debug.Log("This is " + nameof(TestGroupButton4)))
    {
    }
}

[MainToolbarElement]
public class TestGroupButton5 : MainToolbarButtonProvider
{
    public TestGroupButton5() : base(nameof(TestGroupButton5), () => Debug.Log("This is " + nameof(TestGroupButton5)))
    {
    }
}

[MainToolbarElement]
public class TestGroupButton6 : MainToolbarButtonProvider
{
    public TestGroupButton6() : base(nameof(TestGroupButton6), () => Debug.Log("This is " + nameof(TestGroupButton6)))
    {
    }
}