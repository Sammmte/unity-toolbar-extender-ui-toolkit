using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement]
public class SampleComplexElement : VisualElement
{
    public SampleComplexElement()
    {
        style.flexDirection = FlexDirection.Column;
        style.flexGrow = 1;

        var someLabel = new Label("Some Label");
        Add(someLabel);

        for(int i = 0; i < 20; i++)
        {
            var button = new Button(() => Debug.Log("I am button " + (i + 1)));
            button.text = "Button " + (i + 1);
            Add(button);
        }
    }
}