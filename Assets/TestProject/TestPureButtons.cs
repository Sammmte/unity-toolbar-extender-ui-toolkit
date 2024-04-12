using Paps.UnityToolbarExtenderUIToolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement]
public class TestPureButton : Button
{
    public TestPureButton()
    {
        text = "Test Pure Button";
        clicked += () => Debug.Log("I'm a pure button");
    }
}
