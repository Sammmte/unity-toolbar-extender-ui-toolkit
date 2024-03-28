namespace Paps.UnityToolbarExtenderUIToolkit
{
    public static class ServicesAndRepositories
    {
        public static IMainToolbarElementOverridesRepository MainToolbarElementOverridesRepository =
            new InMemoryMainToolbarElementOverridesRepository();

        public static IGroupDefinitionRepository GroupDefinitionRepository =
            new ScriptableObjectGroupDefinitionRepository();
    }
}