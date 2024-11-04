using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor.UIElements;

[MainToolbarElement("SampleColorField")]
public class SampleColorField : ColorField
{
    public void InitializeElement()
    {
        label = "Sample Color";
    }
}
