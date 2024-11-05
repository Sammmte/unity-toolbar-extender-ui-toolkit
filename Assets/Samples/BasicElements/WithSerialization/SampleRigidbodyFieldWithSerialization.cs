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
public class SampleRigidbodyFieldWithSerialization : VisualElement
{
    [Serialize] private Rigidbody _rigidbodyValue;

    private ObjectField _rigidbodyField;
    private Button _instantiateButton;

    // Don't use constructors, use this method instead.
    // Toolbar extender will try to find this method and execute it
    public void InitializeElement()
    {
        _rigidbodyField = new ObjectField("Sample Rigidbody");

        _rigidbodyField.value = _rigidbodyValue;

        // Specify Unity Object type for the field to get the right component
        // Also this helps to filter when using the search tool
        _rigidbodyField.objectType = typeof(Rigidbody);

        // Why this method and not simply RegisterCallback like the other examples?
        // Because RegisterCallback does not work in this case for some strange reason
        // This alternative is practically the same
        _rigidbodyField.RegisterValueChangedCallback(ev =>
        {
            // You need to cast newValue to your Unity Object specific type
            _rigidbodyValue = (Rigidbody)ev.newValue;
        });

        _instantiateButton = new Button();

        _instantiateButton.text = "Instantiate";

        _instantiateButton.clicked += () =>
        {
            if(_rigidbodyValue == null)
            {
                Debug.Log("Rigidbody value is null and could not be instantiated. Assign a valid value and try again.");
                return;
            }    

            Object.Instantiate(_rigidbodyValue);
        };

        Add(_rigidbodyField);
        Add(_instantiateButton);
    }
}
