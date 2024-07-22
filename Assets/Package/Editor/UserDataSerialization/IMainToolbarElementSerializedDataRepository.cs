namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal interface IMainToolbarElementSerializedDataRepository
    {
        MainToolbarElementSerializedData[] GetAll();
        void Set(MainToolbarElementSerializedData[] serializedData);
        void Clear();
    }
}
