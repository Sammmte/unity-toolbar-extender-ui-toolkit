using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine.UIElements;

[MainToolbarElement("SampleToggleWithSerialization")]
public class SampleToggleWithSerialization : Toggle
{
    [Serialize] private bool _toggleValue;

    public SampleToggleWithSerialization() : base("Toggle With Serialization")
    {
        
    }

    public void InitializeElement()
    {
        value = _toggleValue;

        RegisterCallback<ChangeEvent<bool>>(ev => _toggleValue = ev.newValue);
    }
}