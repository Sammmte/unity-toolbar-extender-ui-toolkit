namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal readonly struct MainToolbarElementOverride
    {
        public string ElementId { get; }
        public bool Visible { get; }

        public MainToolbarElementOverride(string elementId, bool visible)
        {
            ElementId = elementId;
            Visible = visible;
        }
    }
}