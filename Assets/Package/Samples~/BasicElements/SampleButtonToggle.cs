﻿using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement(id: "SampleButtonToggle", alignment: ToolbarAlign.Right, order: 0)]
public class SampleButtonToggle : EditorToolbarToggle
{
    public void InitializeElement()
    {
        text = "Sample Button Toggle";
        tooltip = "This is an example of a toolbar button toggle";
        RegisterCallback<ChangeEvent<bool>>(eventArgs => Debug.Log("Sample button toggle changed its value to " + eventArgs.newValue));
    }
}
