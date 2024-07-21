namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal readonly struct MainToolbarElementSerializedKeyValue
    {
        public string Key { get; }
        public object Value { get; }

        public MainToolbarElementSerializedKeyValue(string key, object value)
        {
            Key = key;
            Value = value;
        }
    }
}
