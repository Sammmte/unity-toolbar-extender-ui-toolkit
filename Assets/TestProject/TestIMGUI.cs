using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;

[MainToolbarElement(order: -3)]
public class TestIMGUI : MainToolbarIMGUIContainer
{
    public TestIMGUI()
    {
        onGUIHandler = OnGUI;
    }

    private void OnGUI()
    {
        GUILayout.Label("A GUI LABEL");

        if (GUILayout.Button("A GUI BUTTON"))
        {
            Debug.Log("GUI WORKS HERE");
        }

        GUILayout.Toggle(true, new GUIContent("A GUI TOGGLE"));
    }
}
