using UnityEditor.Toolbars;
using UnityEditor.UIElements;

[Paps.UnityToolbarExtenderUIToolkit.MainToolbarElement(ToolbarExtenderSamplesInfo.SAMPLES_MAIN_TOOLBAR_MENU_BASE + "/SampleColorField")]
public class SampleColorField : ColorField
{
    [UnityEditor.Toolbars.MainToolbarElement(ToolbarExtenderSamplesInfo.SAMPLES_MAIN_TOOLBAR_MENU_BASE + "/SampleColorField", defaultDockPosition = MainToolbarDockPosition.Left)]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummyGroup()
    {
        // Return null here
        return null;
    }
    
    public void InitializeElement()
    {
        label = "Sample Color";
    }
}
