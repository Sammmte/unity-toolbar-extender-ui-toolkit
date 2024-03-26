using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;

[MainToolbarElement]
public class TestSingleButton : MainToolbarButtonProvider
{
    public TestSingleButton() : base(nameof(TestSingleButton), () => Debug.Log("This is " + nameof(TestSingleButton)))
    {
    }
}

[MainToolbarElement]
public class TestSingleButton2 : MainToolbarButtonProvider
{
    public TestSingleButton2() : base(nameof(TestSingleButton2), () => Debug.Log("This is " + nameof(TestSingleButton2)))
    {
    }
}

[MainToolbarElement]
public class TestSingleButton3 : MainToolbarButtonProvider
{
    public TestSingleButton3() : base(nameof(TestSingleButton3), () => Debug.Log("This is " + nameof(TestSingleButton3)))
    {
    }
}

[MainToolbarElement]
public class TestSingleButton4 : MainToolbarButtonProvider
{
    public TestSingleButton4() : base(nameof(TestSingleButton4), () => Debug.Log("This is " + nameof(TestSingleButton4)))
    {
    }
}

[MainToolbarElement(ToolbarAlign.Right)]
public class TestSingleButton5 : MainToolbarButtonProvider
{
    public TestSingleButton5() : base(nameof(TestSingleButton5), () => Debug.Log("This is " + nameof(TestSingleButton5)))
    {

    }
}

[MainToolbarElement(ToolbarAlign.Right)]
public class TestSingleButton6 : MainToolbarButtonProvider
{
    public TestSingleButton6() : base(nameof(TestSingleButton6), () => Debug.Log("This is " + nameof(TestSingleButton6)))
    {
    }
}

[MainToolbarElement(ToolbarAlign.Right)]
public class TestSingleButton7 : MainToolbarButtonProvider
{
    public TestSingleButton7() : base(nameof(TestSingleButton7), () => Debug.Log("This is " + nameof(TestSingleButton7)))
    {
    }
}

[MainToolbarElement(ToolbarAlign.Right)]
public class TestSingleButton8 : MainToolbarButtonProvider
{
    public TestSingleButton8() : base(nameof(TestSingleButton8), () => Debug.Log("This is " + nameof(TestSingleButton8)))
    {
    }
}

[MainToolbarElement(ToolbarAlign.Right)]
public class TestSingleButton9 : MainToolbarButtonProvider
{
    public TestSingleButton9() : base(nameof(TestSingleButton9), () => Debug.Log("This is " + nameof(TestSingleButton9)))
    {
    }
}