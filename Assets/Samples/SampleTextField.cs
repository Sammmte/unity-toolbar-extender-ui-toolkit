using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement(alignWhenSingle: ToolbarAlign.Left, order: 3)]
public class SampleTextField : TextField
{
    public SampleTextField()
    {
        style.flexDirection = FlexDirection.Column;
        label = "Sample Text Field";
        RegisterCallback<ChangeEvent<string>>(eventArgs => Debug.Log("Sample Text Field changed value to " + eventArgs.newValue));
    }
}

[MainToolbarElement(alignWhenSingle: ToolbarAlign.Left, order: 3)]
public class SampleIntegerField : IntegerField
{
    public SampleIntegerField()
    {
        style.flexDirection = FlexDirection.Column;
        label = "Sample Integer Field";
        RegisterCallback<ChangeEvent<int>>(eventArgs => Debug.Log("Sample Integer Field changed value to " + eventArgs.newValue));
    }
}