using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor.Toolbars;

[MainToolbarElement(nameof(MyButtonToggle), order: -4)]
public class MyButtonToggle : EditorToolbarToggle
{
    public MyButtonToggle() : base("HOLO")
    {

    }
}