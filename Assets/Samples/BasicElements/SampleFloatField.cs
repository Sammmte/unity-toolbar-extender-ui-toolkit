using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine.UIElements;

[MainToolbarElement("SampleFloatField", ToolbarAlign.Right)]
public class SampleFloatField : FloatField
{
    public SampleFloatField() : base("Sample Float")
    {

    }
}
