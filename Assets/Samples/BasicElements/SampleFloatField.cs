using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine.UIElements;

[MainToolbarElement("SampleFloatField", ToolbarAlign.Right)]
public class SampleFloatField : FloatField
{
    public void InitializeElement()
    {
        label = "Sample Float";
    }
}
