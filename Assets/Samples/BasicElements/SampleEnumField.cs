using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement("SampleEnumField")]
public class SampleEnumField : EnumField
{
    public SampleEnumField() : base("Sample Enum", KeyCode.A)
    {

    }
}
