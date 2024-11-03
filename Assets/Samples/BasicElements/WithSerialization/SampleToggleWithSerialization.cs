using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine.UIElements;

[MainToolbarElement("SampleToggleWithSerialization")]
public class SampleToggleWithSerialization : Toggle
{
    [Serialize] private bool _toggleValue;

    // Don't use constructors, use this method instead.
    // Toolbar extender will try to find this method and execute it
    public void InitializeElement()
    {
        label = "Toggle With Serialization";
        value = _toggleValue;

        RegisterCallback<ChangeEvent<bool>>(ev => _toggleValue = ev.newValue);
    }
}