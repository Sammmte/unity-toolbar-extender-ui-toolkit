﻿namespace Paps.UnityToolbarExtenderUIToolkit
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

        public static IValueSerializer ValueSerializer =
            new UnitySerializationValueSerializer();

        public static IMainToolbarElementVariableSerializer MainToolbarElementVariableSerializer =
            new JsonMainToolbarElementVariableSerializer(ValueSerializer);

        public static IMainToolbarElementVariableRepository MainToolbarElementVariableRepository =
            new EditorPrefsMainToolbarElementVariableRepository(MainToolbarElementVariableSerializer);
    }
}