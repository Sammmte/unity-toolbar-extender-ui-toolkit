using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[MainToolbarElement("SampleScriptableObjectWithSerialization")]
public class SampleScriptableObjectWithSerialization : ObjectField
{
    private SerializableValue<SampleScriptableObject> _scriptableObjectValue = SerializableValue<SampleScriptableObject>.Create("MyScriptableObject");

    public SampleScriptableObjectWithSerialization() : base("Sample ScriptableObject")
    {
        value = _scriptableObjectValue.Value;

        objectType = typeof(SampleScriptableObject);

        this.RegisterValueChangedCallback(ev =>
        {
            _scriptableObjectValue.Value = (SampleScriptableObject)ev.newValue;
        });
    }
}