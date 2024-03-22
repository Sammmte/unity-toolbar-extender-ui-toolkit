using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;

[MainToolbarElement]
public class TestLeftSingleButton2 : MainToolbarButton
{
    public TestLeftSingleButton2() : base("Test Single Button", () => Debug.Log("Test single button"))
    {
    }
}