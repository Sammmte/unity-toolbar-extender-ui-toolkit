using System.Xml.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    [InitializeOnLoad]
    public static class MainToolbarElementsOverridesApplier
    {
        private static IMainToolbarElementOverridesRepository _mainToolbarElementOverridesRepository;

        static MainToolbarElementsOverridesApplier()
        {
            MainToolbarAutomaticExtender.OnRefresh += ApplyOverrides;
            MainToolbarAutomaticExtender.OnAddedCustomContainersToToolbar += ApplyOverrides;

            _mainToolbarElementOverridesRepository = ServicesAndRepositories.MainToolbarElementOverridesRepository;
        }

        private static void ApplyOverrides()
        {
            var elements = OverridableMainToolbarElementsProvider.GetEligibleElements();

            foreach(var element in elements)
            {
                var possibleOverride = _mainToolbarElementOverridesRepository.Get(element.Id);

                if (possibleOverride == null)
                    continue;

                var overrideValue = possibleOverride.Value;

                EnableOrDisableDisplay(element.VisualElement, overrideValue.Visible);
            }
        }

        private static void EnableOrDisableDisplay(VisualElement visualElement, bool visible)
        {
            visualElement.style.display = visible ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}