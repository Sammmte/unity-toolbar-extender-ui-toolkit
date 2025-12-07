public static class ExampleGroupsProvider
{
    [UnityEditor.Toolbars.MainToolbarElement("MyRootGroup")]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummy()
    {
        // Return null here
        return null;
    }
    
    [UnityEditor.Toolbars.MainToolbarElement("MyRootGroup2")]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummy2()
    {
        // Return null here
        return null;
    }
}