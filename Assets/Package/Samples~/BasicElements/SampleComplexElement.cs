using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

// Configured in Sample Group
[MainToolbarElement(id: "SampleComplexElement")]
public class SampleComplexElement : VisualElement
{
    public void InitializeElement()
    {
        style.flexDirection = FlexDirection.Column;
        style.flexGrow = 1;

        var someLabel = new Label("This is a complex element");
        Add(someLabel);

        for (int i = 0; i < 5; i++)
        {
            int elementNumber = i + 1;
            var button = new Button(() => Debug.Log("I am button " + elementNumber));
            button.text = "Complex Button " + elementNumber;
            Add(button);
        }

        for (int i = 0; i < 5; i++)
        {
            int elementNumber = i + 1;
            var slider = new Slider("Complex Slider " + elementNumber, 0, 100);
            Add(slider);
        }

        for (int i = 0; i < 5; i++)
        {
            var options = new List<string>() { "Option 1", "Option 2" };
            int elementNumber = i + 1;
            var dropdown = new DropdownField("Complex Dropdown " + elementNumber, options, 0);
            Add(dropdown);
        }
    }
}