using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine.UIElements;

[MainToolbarElement("SampleTextField", ToolbarAlign.Right)]
public class SampleTextField : TextField
{
    public void InitializeElement()
    {
        label = "Sample Text";
    }
}