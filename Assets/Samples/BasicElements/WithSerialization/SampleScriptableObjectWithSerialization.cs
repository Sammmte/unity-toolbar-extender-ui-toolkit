using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[MainToolbarElement("SampleScriptableObjectWithSerialization")]
public class SampleScriptableObjectWithSerialization : ObjectField
{
    [Serialize] private SampleScriptableObject _scriptableObjectValue;

    // Don't use constructors, use this method instead.
    // Toolbar extender will try to find this method and execute it
    public void InitializeElement()
    {
        value = _scriptableObjectValue;
        label = "Sample ScriptableObject";

        objectType = typeof(SampleScriptableObject);

        this.RegisterValueChangedCallback(ev =>
        {
            _scriptableObjectValue = (SampleScriptableObject)ev.newValue;
        });
    }
}