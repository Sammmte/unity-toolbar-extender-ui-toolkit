using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement(group: "Left Test Group")]
public class TestButton : Button
{
    public TestButton() : base(() => Debug.Log("This is a test"))
    {
        text = "Test Button";
    }
}
