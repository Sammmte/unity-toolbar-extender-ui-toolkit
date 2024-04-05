namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal interface IMainToolbarElementOverrideRepository
    {
        public MainToolbarElementOverride? Get(string elementId);
        public MainToolbarElementOverride[] GetAll();
        public void Save(MainToolbarElementOverride elementOverride);
        public void Clear();
    }
}