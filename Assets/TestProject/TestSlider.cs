using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement(nameof(TestSlider))]
public class TestSlider : Slider
{
    public TestSlider() : base("Test Slider", 0, 100)
    {
        this.RegisterValueChangedCallback(eventArgs => Debug.Log("Slider value is: " + eventArgs.newValue));
    }
}
