using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement]
public class TestLeftSingleButton : MainToolbarButton
{
    public TestLeftSingleButton() : base("Test Single Button", () => Debug.Log("Test single button"))
    {
    }
}