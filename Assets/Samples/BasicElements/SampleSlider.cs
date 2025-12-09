using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;

[Paps.UnityToolbarExtenderUIToolkit.MainToolbarElement(id: "SampleSlider")]
public class SampleSlider : Slider
{
    [UnityEditor.Toolbars.MainToolbarElement("SampleSlider", defaultDockPosition = MainToolbarDockPosition.Left)]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummyGroup()
    {
        // Return null here
        return null;
    }
    
    public void InitializeElement()
    {
        label = "Sample Slider";
        lowValue = 0;
        highValue = 100;

        RegisterCallback<ChangeEvent<float>>(eventArgs => Debug.Log("Slider value is: " + eventArgs.newValue));
    }
}
