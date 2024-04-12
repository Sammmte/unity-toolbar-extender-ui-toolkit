using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;

[MainToolbarElement]
public class SampleFloatField : IMGUIFloatField
{
    public SampleFloatField() : base("Sample Float Field")
    {
        OnValueChanged += newValue => Debug.Log("Float Field value is: " + newValue);
    }
}
