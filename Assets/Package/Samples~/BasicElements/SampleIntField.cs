using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;

[MainToolbarElement(id: "SampleIntField")]
public class SampleIntField : IMGUIIntField
{
    public SampleIntField() : base("Sample Int Field")
    {
        OnValueChanged += newValue => Debug.Log("Int Field value is: " + newValue);
    }
}
