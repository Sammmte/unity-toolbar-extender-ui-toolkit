namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal interface ISerializableValuesRepository
    {
        public void Save<T>(string saveKey, T value);
        public Maybe<T> Get<T>(string saveKey);
    }
}
