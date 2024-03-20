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

[MainToolbarElement(group: "Left Test Group")]
public class ComplexElement : VisualElement
{
    public ComplexElement()
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