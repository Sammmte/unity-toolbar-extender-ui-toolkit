using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement]
public class TestSingleButton : MainToolbarButton
{
    public TestSingleButton() : base("Test Single Button", () => Debug.Log("Test single button"))
    {
    }
}