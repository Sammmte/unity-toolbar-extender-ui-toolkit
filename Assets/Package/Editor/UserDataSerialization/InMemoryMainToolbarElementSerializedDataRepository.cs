namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class InMemoryMainToolbarElementSerializedDataRepository : IMainToolbarElementSerializedDataRepository
    {
        public MainToolbarElementSerializedData[] GetAll()
        {
            return new MainToolbarElementSerializedData[]
            {
                new MainToolbarElementSerializedData("MyToggle", new MainToolbarElementSerializedKeyValue[]
                {
                    new MainToolbarElementSerializedKeyValue("_myValue", true)
                })
            };
        }
    }
}
