using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

// VERY IMPORTANT NOTE: When desired Unity Object type is derived from Component (like Rigidbody or any MonoBehaviour)
// and the ObjectField is inside a group popup window
// you need to use the new Advanced Object Picker tool
// otherwise classic ObjectSelector window won't be able to find Component derived classes serialized in prefabs
// You can enable new Advanced Object Picker at Preferences -> Search -> Search Engines -> Object Selector Dropdown -> Select 'Advanced'

// You can serialize any Unity Object through ObjectField, even a prefab (GameObject) component, like Rigidbody
// Also any custom Monobehaviour or ScriptableObject
[MainToolbarElement("SampleRigidbodyFieldWithSerialization")]
public class SampleRigidbodyFieldWithSerialization : ObjectField
{
    [Serialize] private Rigidbody _rigidbodyValue;

    // Don't use constructors, use this method instead.
    // Toolbar extender will try to find this method and execute it
    public void InitializeElement()
    {
        label = "Sample Rigidbody";
        value = _rigidbodyValue;

        // Specify Unity Object type for the field to get the right component
        // Also this helps to filter when using the search tool
        objectType = typeof(Rigidbody);

        // Why this method and not simply RegisterCallback like the other examples?
        // Because RegisterCallback does not work in this case for some strange reason
        // This alternative is practically the same
        this.RegisterValueChangedCallback(ev =>
        {
            // You need to cast newValue to your Unity Object specific type
            _rigidbodyValue = (Rigidbody)ev.newValue;
        });
    }
}
