using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine.UIElements;

[MainToolbarElement("SampleIntegerField")]
public class SampleIntegerField : IntegerField
{
    public void InitializeElement()
    {
        label = "Sample Integer";
    }
}
