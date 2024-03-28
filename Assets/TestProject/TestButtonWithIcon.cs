using Paps.UnityToolbarExtenderUIToolkit;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[MainToolbarElement]
public class TestButtonWithIcon : MainToolbarButton
{
    public TestButtonWithIcon() : base((Texture2D)EditorGUIUtility.IconContent("_Popup@2x").image, () => Debug.Log("I have an icon"))
    {
    }
}
