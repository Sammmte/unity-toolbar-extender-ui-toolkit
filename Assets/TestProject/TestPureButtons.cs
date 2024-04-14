using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement(nameof(TestPureButton))]
public class TestPureButton : Button
{
    public TestPureButton()
    {
        text = "Test Pure Button";
        clicked += () => Debug.Log("I'm a pure button");
    }
}
