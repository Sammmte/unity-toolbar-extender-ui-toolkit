namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal abstract class RecommendedStyle
    {
        private bool _appliedRootElementStyle;
        protected bool ApplyGroupStyleAlways;

        public void Apply(bool isInsideGroup)
        {
            if ((isInsideGroup && _appliedRootElementStyle) || (isInsideGroup && ApplyGroupStyleAlways))
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