using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement(id: "SampleIMGUIContainer", alignWhenSingle: ToolbarAlign.Right, order: 1)]
public class SampleIMGUIContainer : IMGUIContainer
{
    public SampleIMGUIContainer() // you can also construct your object with base()
    {
        onGUIHandler = MyGUIMethod;
    }

    private void MyGUIMethod()
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("GUI Button"))
            Debug.Log("GUI Button clicked");

        GUILayout.EndHorizontal();
    }
}