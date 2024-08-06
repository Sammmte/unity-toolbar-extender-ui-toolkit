namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal readonly struct MainToolbarElementSerializedKeyValue
    {
        public string Key { get; }
        public string SerializedValue { get; }

        public MainToolbarElementSerializedKeyValue(string key, string serializedValue)
        {
            Key = key;
            SerializedValue = serializedValue;
        }
    }
}
