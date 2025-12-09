using UnityEditor.Toolbars;
using UnityEngine;

public static class ExampleGroupsProviderForSubgroups
{
    [UnityEditor.Toolbars.MainToolbarElement(ToolbarExtenderSamplesInfo.SAMPLES_MAIN_TOOLBAR_MENU_BASE + "/Groups/SampleGroupWithSubgroups", defaultDockPosition = MainToolbarDockPosition.Right)]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummyGroup()
    {
        // Return null here
        return null;
    }
}
