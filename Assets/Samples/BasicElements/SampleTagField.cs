using UnityEditor.Toolbars;
using UnityEditor.UIElements;

[Paps.UnityToolbarExtenderUIToolkit.MainToolbarElement(ToolbarExtenderSamplesInfo.SAMPLES_MAIN_TOOLBAR_MENU_BASE + "/SampleTagField")]
public class SampleTagField : TagField
{
    [UnityEditor.Toolbars.MainToolbarElement(ToolbarExtenderSamplesInfo.SAMPLES_MAIN_TOOLBAR_MENU_BASE + "/SampleTagField", defaultDockPosition = MainToolbarDockPosition.Left)]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummyGroup()
    {
        // Return null here
        return null;
    }
    
    public void InitializeElement()
    {
        label = "Sample Tag";
    }
}