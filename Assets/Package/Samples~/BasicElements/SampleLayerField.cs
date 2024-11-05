using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor.UIElements;

[MainToolbarElement("SampleLayerField")]
public class SampleLayerField : LayerField
{
    public void InitializeElement()
    {
        label = "Sample Layer";
    }
}
