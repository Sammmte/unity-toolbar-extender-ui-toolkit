using UnityEditor.Toolbars;
using UnityEngine;

public static class ExampleGroupsProvider
{
    [UnityEditor.Toolbars.MainToolbarElement("SampleRootGroup", defaultDockPosition = MainToolbarDockPosition.Left)]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummyGroup()
    {
        // Return null here
        return null;
    }
    
    [UnityEditor.Toolbars.MainToolbarElement("UnityTypesElementsGroup", defaultDockPosition = MainToolbarDockPosition.Left)]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummyGroup2()
    {
        // Return null here
        return null;
    }
}