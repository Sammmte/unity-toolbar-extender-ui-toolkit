namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal interface IMainToolbarElementVariableSerializer
    {
        public string Serialize(SerializableElementGroup serializableElementGroup);
        public SerializableElementGroup Deserialize(string serializedElementGroup);
    }
}
