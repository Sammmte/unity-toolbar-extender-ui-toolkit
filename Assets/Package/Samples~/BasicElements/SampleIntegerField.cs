using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine.UIElements;

[MainToolbarElement("SampleIntegerField", ToolbarAlign.Right)]
public class SampleIntegerField : IntegerField
{
    public SampleIntegerField() : base("Sample Integer")
    {
        
    }
}
