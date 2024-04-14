using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement(nameof(TestIMGUI), order: -3)]
public class TestIMGUI : IMGUIContainer
{
    public TestIMGUI()
    {
        onGUIHandler = OnGUI;
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();

        GUILayout.Label("A GUI LABEL");

        if (GUILayout.Button("A GUI BUTTON"))
        {
            Debug.Log("GUI WORKS HERE");
        }

        GUILayout.Toggle(true, new GUIContent("A GUI TOGGLE"));

        GUILayout.EndHorizontal();
    }
}
