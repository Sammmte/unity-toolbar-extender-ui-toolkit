using UnityEditor.Toolbars;

public static class ExampleGroupsProviderForGroupsWithSerialization
{
    [UnityEditor.Toolbars.MainToolbarElement(ToolbarExtenderSamplesInfo.SAMPLES_MAIN_TOOLBAR_MENU_BASE + "/Groups/ElementsWithSerialization", defaultDockPosition = MainToolbarDockPosition.Right)]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummyGroup()
    {
        // Return null here
        return null;
    }
}