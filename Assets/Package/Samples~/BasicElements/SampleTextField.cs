﻿using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;

[MainToolbarElement(id: "SampleTextField")]
public class SampleTextField : IMGUITextField
{
    public SampleTextField() : base("Sample Text Field")
    {
        OnValueChanged += newValue => Debug.Log("Text Field value is: " + newValue);
    }
}