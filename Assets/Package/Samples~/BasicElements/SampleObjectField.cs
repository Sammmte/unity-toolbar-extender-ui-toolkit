using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor.UIElements;

[MainToolbarElement("SampleObjectField")]
public class SampleObjectField : ObjectField
{
    public void InitializeElement()
    {
        label = "Sample Object";
    }
}
