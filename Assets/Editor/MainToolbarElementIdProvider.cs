using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public static class MainToolbarElementIdProvider
    {
        public static string IdOf(VisualElement visualElement)
        {
            if (HasFixedId(visualElement, out string id))
                return id;

            var idByName = string.IsNullOrEmpty(visualElement.name) ? null : visualElement.name;

            return idByName;
        }

        private static bool HasFixedId(VisualElement visualElement, out string id)
        {
            id = UnityNativeElementsIds.IdOf(visualElement);

            return id != null;
        }
    }
}