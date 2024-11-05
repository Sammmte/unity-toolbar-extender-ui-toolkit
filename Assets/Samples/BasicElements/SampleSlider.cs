using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement(id: "SampleSlider")]
public class SampleSlider : Slider
{
    public void InitializeElement()
    {
        label = "Sample Slider";
        lowValue = 0;
        highValue = 100;

        RegisterCallback<ChangeEvent<float>>(eventArgs => Debug.Log("Slider value is: " + eventArgs.newValue));
    }
}
