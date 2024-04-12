using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement]
public class SampleSlider : Slider
{
    public SampleSlider() : base("Sample Slider", 0, 100)
    {
        RegisterCallback<ChangeEvent<float>>(newValue => Debug.Log("Slider value is: " + newValue));
    }
}
