namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal readonly struct MainToolbarElementSerializedData
    {
        public string Id { get; }
        public MainToolbarElementSerializedKeyValue[] KeyValues { get; }

        public MainToolbarElementSerializedData(string id, MainToolbarElementSerializedKeyValue[] keyValues)
        {
            Id = id;
            KeyValues = keyValues;
        }
    }
}
