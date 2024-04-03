using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor;
using UnityEngine;

[MainToolbarElement(order: -4)]
public class MyButtonToggle : MainToolbarButtonToggle
{
    public MyButtonToggle() : base("HOLO")
    {
        
    }
}