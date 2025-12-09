using UnityEditor.Toolbars;
using UnityEditor.UIElements;

[Paps.UnityToolbarExtenderUIToolkit.MainToolbarElement(ToolbarExtenderSamplesInfo.SAMPLES_MAIN_TOOLBAR_MENU_BASE + "/SampleObjectField")]
public class SampleObjectField : ObjectField
{
    [UnityEditor.Toolbars.MainToolbarElement(ToolbarExtenderSamplesInfo.SAMPLES_MAIN_TOOLBAR_MENU_BASE + "/SampleObjectField", defaultDockPosition = MainToolbarDockPosition.Left)]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummyGroup()
    {
        // Return null here
        return null;
    }
    
    public void InitializeElement()
    {
        label = "Sample Object";
    }
}
