using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;
using UnityEngine.UIElements;

// Configured in Sample Group
[MainToolbarElement]
public class SampleComplexElement : VisualElement
{
    public SampleComplexElement()
    {
        style.flexDirection = FlexDirection.Column;
        style.flexGrow = 1;

        var someLabel = new Label("This is a complex element");
        Add(someLabel);

        for(int i = 0; i < 20; i++)
        {
            int buttonNumber = i + 1;
            var button = new Button(() => Debug.Log("I am button " + buttonNumber));
            button.text = "Button " + buttonNumber;
            Add(button);
        }
    }
}