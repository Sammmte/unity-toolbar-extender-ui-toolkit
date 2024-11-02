namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal interface IMainToolbarElementVariableRepository
    {
        public void Set(SerializableElement serializableElement);
        public SerializableElement Get(MainToolbarElement element);
        public void Save();
    }
}
