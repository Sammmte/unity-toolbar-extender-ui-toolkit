using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;

[MainToolbarElement(alignWhenSingle: ToolbarAlign.Right, order: 1)]
public class SampleIMGUIContainer : MainToolbarIMGUIContainer
{
    public SampleIMGUIContainer() // you can also construct your object with base()
    {
        onGUIHandler = MyGUIMethod;
    }

    private void MyGUIMethod()
    {
        if (GUILayout.Button("GUI Button"))
            Debug.Log("GUI Button clicked");
    }
}