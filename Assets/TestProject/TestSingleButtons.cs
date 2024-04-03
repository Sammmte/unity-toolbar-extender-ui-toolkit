using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor.Toolbars;
using UnityEngine;

[MainToolbarElement]
public class TestSingleButton : EditorToolbarButton
{
    public TestSingleButton() : base(nameof(TestSingleButton), () => Debug.Log("This is " + nameof(TestSingleButton)))
    {
    }
}

[MainToolbarElement]
public class TestSingleButton2 : EditorToolbarButton
{
    public TestSingleButton2() : base(nameof(TestSingleButton2), () => Debug.Log("This is " + nameof(TestSingleButton2)))
    {
    }
}

[MainToolbarElement]
public class TestSingleButton3 : EditorToolbarButton
{
    public TestSingleButton3() : base(nameof(TestSingleButton3), () => Debug.Log("This is " + nameof(TestSingleButton3)))
    {
    }
}

[MainToolbarElement]
public class TestSingleButton4 : EditorToolbarButton
{
    public TestSingleButton4() : base(nameof(TestSingleButton4), () => Debug.Log("This is " + nameof(TestSingleButton4)))
    {
    }
}

[MainToolbarElement(ToolbarAlign.Right)]
public class TestSingleButton5 : EditorToolbarButton
{
    public TestSingleButton5() : base(nameof(TestSingleButton5), () => Debug.Log("This is " + nameof(TestSingleButton5)))
    {

    }
}

[MainToolbarElement(ToolbarAlign.Right)]
public class TestSingleButton6 : EditorToolbarButton
{
    public TestSingleButton6() : base(nameof(TestSingleButton6), () => Debug.Log("This is " + nameof(TestSingleButton6)))
    {
    }
}

[MainToolbarElement(ToolbarAlign.Right)]
public class TestSingleButton7 : EditorToolbarButton
{
    public TestSingleButton7() : base(nameof(TestSingleButton7), () => Debug.Log("This is " + nameof(TestSingleButton7)))
    {
    }
}

[MainToolbarElement(ToolbarAlign.Right)]
public class TestSingleButton8 : EditorToolbarButton
{
    public TestSingleButton8() : base(nameof(TestSingleButton8), () => Debug.Log("This is " + nameof(TestSingleButton8)))
    {
    }
}

[MainToolbarElement(ToolbarAlign.Right)]
public class TestSingleButton9 : EditorToolbarButton
{
    public TestSingleButton9() : base(nameof(TestSingleButton9), () => Debug.Log("This is " + nameof(TestSingleButton9)))
    {
    }
}