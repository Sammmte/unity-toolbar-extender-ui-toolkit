using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement(id: "SampleIMGUIContainer", alignment: ToolbarAlign.Right, order: 1)]
public class SampleIMGUIContainer : IMGUIContainer
{
    public void InitializeElement()
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