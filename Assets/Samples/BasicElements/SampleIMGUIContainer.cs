using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;

[Paps.UnityToolbarExtenderUIToolkit.MainToolbarElement(id: ToolbarExtenderSamplesInfo.SAMPLES_MAIN_TOOLBAR_MENU_BASE + "/SampleIMGUIContainer", order: 1)]
public class SampleIMGUIContainer : IMGUIContainer
{
    [UnityEditor.Toolbars.MainToolbarElement(ToolbarExtenderSamplesInfo.SAMPLES_MAIN_TOOLBAR_MENU_BASE + "/SampleIMGUIContainer", defaultDockPosition = MainToolbarDockPosition.Left)]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummyGroup()
    {
        // Return null here
        return null;
    }
    
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