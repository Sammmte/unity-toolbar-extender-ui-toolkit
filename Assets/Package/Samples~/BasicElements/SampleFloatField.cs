using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine.UIElements;

[MainToolbarElement("SampleFloatField")]
public class SampleFloatField : FloatField
{
    public void InitializeElement()
    {
        label = "Sample Float";
    }
}
