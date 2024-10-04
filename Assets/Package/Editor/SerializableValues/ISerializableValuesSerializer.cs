namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal interface ISerializableValuesSerializer
    {
        Maybe<string> Serialize<T>(T value);
        Maybe<T> Deserialize<T>(string serializedValue);
    }
}
