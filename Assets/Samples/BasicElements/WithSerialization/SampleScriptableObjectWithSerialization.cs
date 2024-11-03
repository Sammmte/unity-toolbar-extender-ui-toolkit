using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[MainToolbarElement("SampleScriptableObjectWithSerialization")]
public class SampleScriptableObjectWithSerialization : ObjectField
{
    [Serialize] private SampleScriptableObject _scriptableObjectValue;

    public SampleScriptableObjectWithSerialization() : base("Sample ScriptableObject")
    {
        
    }

    public void InitializeElement()
    {
        value = _scriptableObjectValue;

        objectType = typeof(SampleScriptableObject);

        this.RegisterValueChangedCallback(ev =>
        {
            _scriptableObjectValue = (SampleScriptableObject)ev.newValue;
        });
    }
}