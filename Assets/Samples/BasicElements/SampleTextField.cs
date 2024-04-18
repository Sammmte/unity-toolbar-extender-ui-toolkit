using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine.UIElements;

[MainToolbarElement("SampleTextField", ToolbarAlign.Right)]
public class SampleTextField : TextField
{
    public SampleTextField() : base("Sample Text")
    {

    }
}