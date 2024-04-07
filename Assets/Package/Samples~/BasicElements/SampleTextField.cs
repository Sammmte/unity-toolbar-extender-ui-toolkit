using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;
using UnityEngine.UIElements;

// Configured in Sample Group
[MainToolbarElement(alignWhenSingle: ToolbarAlign.Left, order: 3)]
public class SampleTextField : TextField
{
    public SampleTextField()
    {
        label = "Sample Text Field";
        RegisterCallback<ChangeEvent<string>>(eventArgs => Debug.Log("Sample Text Field changed value to " + eventArgs.newValue));
    }
}
