using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement(id: "SampleSlider")]
public class SampleSlider : Slider
{
    public SampleSlider() : base("Sample Slider", 0, 100)
    {
        RegisterCallback<ChangeEvent<float>>(eventArgs => Debug.Log("Slider value is: " + eventArgs.newValue));
    }
}
