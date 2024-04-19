namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal abstract class RecommendedStyle
    {
        public void Apply(bool isInsideGroup)
        {
            if (isInsideGroup)
                ApplyInsideGroupStyle();
            else
                ApplyRootElementStyle();
        }

        protected virtual void ApplyRootElementStyle() { }
        protected virtual void ApplyInsideGroupStyle() { }
    }
}