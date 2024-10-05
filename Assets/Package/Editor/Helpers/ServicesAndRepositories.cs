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

        public static ISerializableValuesSerializer SerializableValuesSerializer =
            new MultiJsonSerializableValuesSerializer();

        public static ISerializableValuesRepository SerializableValuesRepository =
            new JsonEditorPrefsSerializableValuesRepository(SerializableValuesSerializer);
    }
}