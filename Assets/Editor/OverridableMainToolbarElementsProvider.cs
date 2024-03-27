using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public static class OverridableMainToolbarElementsProvider
    {
        public static OverridableMainToolbarElement[] GetEligibleElements()
        {
            if(!ToolbarWrapper.IsAvailable)
                return new OverridableMainToolbarElement[0];

            return ToolbarWrapper.LeftContainer.Children()
                .Concat(ToolbarWrapper.RightContainer.Children())
                .Concat(ToolbarWrapper.PlayModeButtonsContainer.Children())
                .Concat(MainToolbarAutomaticExtender.CustomMainToolbarElements)
                .Concat(MainToolbarAutomaticExtender.GroupElements)
                .Select(visualElement => new OverridableMainToolbarElement(
                    MainToolbarElementIdProvider.IdOf(visualElement),
                    visualElement
                    )
                )
                .Where(controller => controller.Id != null)
                .GroupBy(controller => controller.Id)
                .Select(group => group.First())
                .ToArray();
        }
    }
}