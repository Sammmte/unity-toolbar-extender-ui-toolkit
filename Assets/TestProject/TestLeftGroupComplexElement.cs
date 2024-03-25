﻿using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement(group: TestGroups.LeftGroup)]
public class TestLeftGroupComplexElement : VisualElement
{
    public TestLeftGroupComplexElement()
    {
        Add(new Label("This is a complex element"));

        var button1 = new Button(() => Debug.Log("ComplexElement button pressed"));
        button1.text = "Button 1";

        var button2 = new Button(() => Debug.Log("another ComplexElement button pressed"));
        button2.text = "Button 2";

        Add(button1);
        Add(button2);
        Add(new Toggle("Just a toggle"));
    }
}