using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine.UIElements;

[MainToolbarElement("SampleIntegerField", ToolbarAlign.Right)]
public class SampleIntegerField : IntegerField
{
    public void InitializeElement()
    {
        label = "Sample Integer";
    }
}
