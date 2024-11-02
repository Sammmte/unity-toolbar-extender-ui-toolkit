namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal interface IMainToolbarElementVariableRepository
    {
        public void Set(SerializableElement serializableElement);
        public SerializableElement[] GetAll();
        public void Save();
    }
}
