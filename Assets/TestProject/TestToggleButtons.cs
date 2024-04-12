using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor.Toolbars;

[MainToolbarElement(order: -4)]
public class MyButtonToggle : EditorToolbarToggle
{
    public MyButtonToggle() : base("HOLO")
    {

    }
}