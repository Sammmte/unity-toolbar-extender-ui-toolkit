using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement(alignWhenSingle: ToolbarAlign.Left, order: 3)]
public class SampleIntegerField : IntegerField
{
    public SampleIntegerField()
    {
        label = "Sample Integer Field";
        RegisterCallback<ChangeEvent<int>>(eventArgs => Debug.Log("Sample Integer Field changed value to " + eventArgs.newValue));
    }
}