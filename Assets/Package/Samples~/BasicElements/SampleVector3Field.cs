using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine.UIElements;

[MainToolbarElement("SampleVector3Field")]
public class SampleVector3Field : Vector3Field
{
    public void InitializeElement()
    {
        label = "Sample Vector 3";
    }
}
