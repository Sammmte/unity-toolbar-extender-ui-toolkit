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

        public static IValueSerializer ValueSerializer =
            new UnitySerializationValueSerializer();

        public static IMainToolbarElementVariableSerializer MainToolbarElementVariableSerializer =
            new JsonMainToolbarElementVariableSerializer(ValueSerializer);

        public static IMainToolbarElementVariableRepository MainToolbarElementVariableRepository =
            new UserSettingsFileMainToolbarElementVariableRepository(MainToolbarElementVariableSerializer);
    }
}