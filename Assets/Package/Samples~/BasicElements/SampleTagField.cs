using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor.UIElements;

[MainToolbarElement("SampleTagField")]
public class SampleTagField : TagField
{
    public void InitializeElement()
    {
        label = "Sample Tag";
    }
}