using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine.UIElements;

[MainToolbarElement]
public class TestSlider : Slider
{
    public TestSlider() : base("Test Slider", 0, 100)
    {

    }
}
