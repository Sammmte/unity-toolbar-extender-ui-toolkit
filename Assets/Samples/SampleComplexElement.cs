using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement]
public class SampleComplexElement : VisualElement
{
    public SampleComplexElement()
    {
        var someLabel = new Label("Some Label");

        var someButton = new Button(() => Debug.Log("Some button inside a complex visual element"));
        someButton.text = "Some Button";

        var yetAnotherButton = new Button(() => Debug.Log("Button that does something different"));
        yetAnotherButton.text = "Yet Another Button";

        style.flexDirection = FlexDirection.Column;
        style.flexGrow = 1;

        Add(someLabel);
        Add(someButton);
        Add(yetAnotherButton);
    }
}