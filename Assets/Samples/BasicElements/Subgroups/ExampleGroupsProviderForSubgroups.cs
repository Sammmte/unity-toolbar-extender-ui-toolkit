using UnityEditor.Toolbars;
using UnityEngine;

public static class ExampleGroupsProviderForSubgroups
{
    [UnityEditor.Toolbars.MainToolbarElement("SampleGroupWithSubgroups", defaultDockPosition = MainToolbarDockPosition.Right)]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummyGroup()
    {
        // Return null here
        return null;
    }
}
