using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;

[MainToolbarElement]
public class TestLeftSingleButton3 : MainToolbarButton
{
    public TestLeftSingleButton3() : base("Test Single Button", () => Debug.Log("Test single button"))
    {
    }
}