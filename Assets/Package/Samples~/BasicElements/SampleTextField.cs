using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine.UIElements;

[MainToolbarElement("SampleTextField")]
public class SampleTextField : TextField
{
    public void InitializeElement()
    {
        label = "Sample Text";
    }
}