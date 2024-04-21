using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class MainToolbarElementOverrideApplier
    {
        private class HiddenRemovedElement
        {
            public VisualElement RemovedVisualElement;
            public VisualElement Parent;
            public int Index;
        }

        private static readonly string[] EXCEPTIONAL_ELEMENTS_FOR_VISIBILITY =
        {
            UnityNativeElementsIds.ACCOUNT_DROPDOWN_ID,
            UnityNativeElementsIds.CLOUD_BUTTON_ID
        };
        private readonly IMainToolbarElementOverrideRepository _mainToolbarElementOverrideRepository;
        private MainToolbarElement[] _mainToolbarElements = new MainToolbarElement[0];
        private NativeToolbarElement[] _nativeElements = new NativeToolbarElement[0];
        private readonly Dictionary<string, MainToolbarElementOverride> _nativeElementsInitialState = new Dictionary<string, MainToolbarElementOverride>();
        private readonly Dictionary<string, HiddenRemovedElement> _hiddenElementsByRemotion = new Dictionary<string, HiddenRemovedElement>();

        public MainToolbarElementOverrideApplier(IMainToolbarElementOverrideRepository mainToolbarElementOverrideRepository)
        {
            _mainToolbarElementOverrideRepository = mainToolbarElementOverrideRepository;
        }

        public void SetNativeElements(NativeToolbarElement[] nativeElements)
        {
            _nativeElements = nativeElements.ToArray();

            SaveNativeElementsInitialState();
            SetNativeElementsDefaultState(); // necesito hacer esto?
        }

        public void SetCustomElements(MainToolbarElement[] mainToolbarElements)
        {
            _mainToolbarElements = mainToolbarElements.ToArray();
        }

        public void ApplyOverrides()
        {
            SetNativeElementsDefaultState();

            ApplyOverridesOnCustomElements();
            ApplyOverridesOnNativeElements();
        }

        private void ApplyOverridesOnCustomElements()
        {
            foreach (var mainToolbarElement in _mainToolbarElements)
            {
                ApplyOverride(mainToolbarElement.Id, mainToolbarElement.VisualElement);
            }
        }

        private void ApplyOverridesOnNativeElements()
        {
            foreach (var nativeElement in _nativeElements)
            {
                ApplyOverride(nativeElement.Id, nativeElement.VisualElement);
            }
        }

        private void SaveNativeElementsInitialState()
        {
            if (_nativeElementsInitialState.Count > 0)
                return;

            foreach (var nativeElement in _nativeElements)
            {
                _nativeElementsInitialState[nativeElement.Id] =
                    new MainToolbarElementOverride(
                        nativeElement.Id,
                        nativeElement.VisualElement.resolvedStyle.display == DisplayStyle.Flex
                        );
            }
        }

        private void SetNativeElementsDefaultState()
        {
            foreach (var nativeElement in _nativeElements)
            {
                var defaultStateOverride = _nativeElementsInitialState[nativeElement.Id];

                ApplyOverride(nativeElement, defaultStateOverride);
            }
        }

        private void ApplyOverride(NativeToolbarElement nativeElement, MainToolbarElementOverride overrideData)
        {
            ApplyVisibilityOverride(nativeElement.Id, nativeElement.VisualElement, overrideData.Visible);
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
            if (IsExceptionElementForVisibility(elementId))
                HandleVisibilityApplicationOnExceptions(elementId, visualElement, visible);
            else
                visualElement.style.display = visible ? DisplayStyle.Flex : DisplayStyle.None;
        }

        private bool IsExceptionElementForVisibility(string elementId)
        {
            return EXCEPTIONAL_ELEMENTS_FOR_VISIBILITY.Contains(elementId);
        }

        private void HandleVisibilityApplicationOnExceptions(string elementId, VisualElement visualElement, bool visible)
        {
            if (visible)
                HandleExceptionalElementVisibleCase(elementId, visualElement);
            else
                HandleExceptionalElementInvisibleCase(elementId, visualElement);
        }

        private void HandleExceptionalElementInvisibleCase(string elementId, VisualElement visualElement)
        {
            var parent = visualElement.parent;
            var index = parent.IndexOf(visualElement);

            if (parent.Contains(visualElement))
                parent.Remove(visualElement);

            if (!_hiddenElementsByRemotion.ContainsKey(elementId))
                _hiddenElementsByRemotion.Add(elementId, new HiddenRemovedElement()
                {
                    RemovedVisualElement = visualElement,
                    Parent = parent,
                    Index = index
                });
        }

        private void HandleExceptionalElementVisibleCase(string elementId, VisualElement visualElement)
        {
            if (_hiddenElementsByRemotion.ContainsKey(elementId))
            {
                var removedElement = _hiddenElementsByRemotion[elementId];

                var parent = removedElement.Parent;

                if (!parent.Contains(visualElement))
                {
                    var bestIndex = GetIndexEqualOrLessThan(removedElement.Index, parent);

                    parent.Insert(bestIndex, visualElement);
                }

                _hiddenElementsByRemotion.Remove(elementId);
            }
        }

        private int GetIndexEqualOrLessThan(int removedElementIndex, VisualElement parent)
        {
            for (int i = parent.childCount - 1; i >= 0; i--)
            {
                if (removedElementIndex <= i)
                    return i;
            }

            return 0;
        }
    }
}