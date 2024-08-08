namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal static class ServicesAndRepositories
    {
        public static IMainToolbarElementOverrideRepository MainToolbarElementOverridesRepository =
            new JsonEditorPrefsMainToolbarElementOverrideRepository();

        public static IGroupDefinitionRepository GroupDefinitionRepository =
            new ScriptableObjectGroupDefinitionRepository();

        public static IMainToolbarElementRepository MainToolbarElementRepository =
            new ByAttributeMainToolbarElementRepository();

        public static IMainToolbarElementSerializedDataRepository MainToolbarElementSerializedDataRepository =
            new JsonEditorPrefsMainToolbarElementSerializedDataRepository();

        public static IMainToolbarElementDataSerializer MainToolbarElementDataSerializer =
            new JsonMainToolbarElementDataSerializer();

        public static IClonator Clonator = new JsonClonator();
    }
}