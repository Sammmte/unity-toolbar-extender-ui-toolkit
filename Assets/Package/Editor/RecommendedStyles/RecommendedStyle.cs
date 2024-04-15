namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal abstract class RecommendedStyle
    {
        private bool _appliedRootElementStyle;

        public void Apply(bool isInsideGroup)
        {
            if (isInsideGroup && _appliedRootElementStyle)
            {
                _appliedRootElementStyle = false;
                ApplyInsideGroupStyle();
            }
            else if(!isInsideGroup)
            {
                _appliedRootElementStyle = true;
                ApplyRootElementStyle();
            }
        }

        protected abstract void ApplyRootElementStyle();
        protected abstract void ApplyInsideGroupStyle();
    }
}