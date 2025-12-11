using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class MainToolbarElementOverrideApplier
    {
        private readonly IMainToolbarElementOverrideRepository _mainToolbarElementOverrideRepository;
        private MainToolbarElement[] _mainToolbarElements = new MainToolbarElement[0];

        public MainToolbarElementOverrideApplier(IMainToolbarElementOverrideRepository mainToolbarElementOverrideRepository)
        {
            _mainToolbarElementOverrideRepository = mainToolbarElementOverrideRepository;
        }

        public void SetCustomElements(MainToolbarElement[] mainToolbarElements)
        {
            _mainToolbarElements = mainToolbarElements.ToArray();
        }

        public void ApplyOverrides()
        {
            ApplyOverridesOnCustomElements();
        }

        private void ApplyOverridesOnCustomElements()
        {
            foreach (var mainToolbarElement in _mainToolbarElements)
            {
                ApplyOverride(mainToolbarElement.Id, mainToolbarElement.VisualElement);
            }
        }

        private void ApplyOverride(string id, VisualElement visualElement)
        {
            var userOverride = _mainToolbarElementOverrideRepository
                .Get(id);

            if (userOverride == null)
                return;

            ApplyVisibilityOverride(id, visualElement, userOverride.Value.Visible);
        }

        private void ApplyVisibilityOverride(string elementId, VisualElement visualElement, bool visible)
        {
            visualElement.style.display = visible ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}