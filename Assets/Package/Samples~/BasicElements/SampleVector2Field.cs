using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine.UIElements;

[MainToolbarElement("SampleVector2Field")]
public class SampleVector2Field : Vector2Field
{
    public void InitializeElement()
    {
        label = "Sample Vector 2";
    }
}