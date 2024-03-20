using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor.Toolbars;
using UnityEngine;

[MainToolbarElement(group: "EL MAS PIJA")]
public class TestButton : EditorToolbarButton
{
    public TestButton() : base(() => Debug.Log("This is a test"))
    {
        text = "Test Button";
    }
}

[MainToolbarElement(group: "EL MAS PIJA")]
public class TestButton1 : EditorToolbarButton
{
    public TestButton1() : base(() => Debug.Log("This is a test"))
    {
        text = "Test Button";
    }
}

[MainToolbarElement(group: "EL MAS CHORI")]
public class TestButton2 : EditorToolbarButton
{
    public TestButton2() : base(() => Debug.Log("This is a test"))
    {
        text = "Test Button";
    }
}

[MainToolbarElement(group: "EL MAS PIJA")]
public class TestButton3 : EditorToolbarButton
{
    public TestButton3() : base(() => Debug.Log("This is a test"))
    {
        text = "Test Button";
    }
}

[MainToolbarElement(group: "EL MAS PIJA")]
public class TestButton4 : EditorToolbarButton
{
    public TestButton4() : base(() => Debug.Log("This is a test"))
    {
        text = "Test Button";
    }
}

[MainToolbarElement(group: "EL MAS PIJA")]
public class TestButton5 : EditorToolbarButton
{
    public TestButton5() : base(() => Debug.Log("This is a test"))
    {
        text = "Test Button";
    }
}

[MainToolbarElement(align: ToolbarAlign.Right, group: "EL MAS PIJA")]
public class TestButton6 : EditorToolbarButton
{
    public TestButton6() : base(() => Debug.Log("AAAAAAAAA"))
    {
        text = "Test Button";
    }
}