using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine.UIElements;

[MainToolbarElement("SampleToggleWithSerialization")]
public class SampleToggleWithSerialization : Toggle
{
    private SerializableValue<bool> _toggleValue = SerializableValue<bool>.Create("MyToggleValue");

    public SampleToggleWithSerialization() : base("Toggle With Serialization")
    {
        value = _toggleValue.Value;

        RegisterCallback<ChangeEvent<bool>>(ev => _toggleValue.Value = ev.newValue);
    }
}
