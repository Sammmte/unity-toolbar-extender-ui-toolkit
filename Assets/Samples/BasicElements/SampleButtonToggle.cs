using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;
using MainToolbarElement = Paps.UnityToolbarExtenderUIToolkit.MainToolbarElementAttribute;

[MainToolbarElement(id: ToolbarExtenderSamplesInfo.SAMPLES_MAIN_TOOLBAR_MENU_BASE + "/SampleButtonToggle", order: 0)]
public class SampleButtonToggle : EditorToolbarToggle
{
    [UnityEditor.Toolbars.MainToolbarElement(ToolbarExtenderSamplesInfo.SAMPLES_MAIN_TOOLBAR_MENU_BASE + "/SampleButtonToggle", defaultDockPosition = MainToolbarDockPosition.Middle)]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummyGroup()
    {
        // Return null here
        return null;
    }
    
    public void InitializeElement()
    {
        text = "Sample Button Toggle";
        tooltip = "This is an example of a toolbar button toggle";
        RegisterCallback<ChangeEvent<bool>>(eventArgs => Debug.Log("Sample button toggle changed its value to " + eventArgs.newValue));
    }
}
