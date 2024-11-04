using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement("SampleEnumField")]
public class SampleEnumField : EnumField
{
    public void InitializeElement()
    {
        label = "Sample Enum";
        Init(KeyCode.A);
    }
}
