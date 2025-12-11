using UnityEditor.Toolbars;

public static class ExampleGroupsProviderForGroupsWithSerialization
{
    [UnityEditor.Toolbars.MainToolbarElement("ElementsWithSerialization", defaultDockPosition = MainToolbarDockPosition.Right)]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummyGroup()
    {
        // Return null here
        return null;
    }
}