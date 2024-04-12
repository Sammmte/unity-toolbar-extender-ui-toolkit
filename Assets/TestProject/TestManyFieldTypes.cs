using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;

[MainToolbarElement(order: -30)]
public class TestFloatField : IMGUIFloatField
{
    public TestFloatField() : base("Test Float")
    {
        OnValueChanged += newValue => Debug.Log("Float Field changed value: " + newValue);
    }
}

[MainToolbarElement(order: -30)]
public class TestIntField : IMGUIIntField
{
    public TestIntField() : base("Test Int")
    {
        OnValueChanged += newValue => Debug.Log("Int Field changed value: " + newValue);
    }
}

[MainToolbarElement(order: -30)]
public class TestTextField : IMGUITextField
{
    public TestTextField() : base("Test Text")
    {
        OnValueChanged += newValue => Debug.Log("Text Field changed value: " + newValue);
    }
}