namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal interface IValueSerializer
    {
        string Serialize<T>(T value);
        T Deserialize<T>(string serializedValue);
    }
}
