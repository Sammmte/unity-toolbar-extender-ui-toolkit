using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class OrganizationalFoldableContainer : Box
    {
        private static readonly Color ORGANIZATIONAL_FOLDABLE_CONTAINER_BORDER_COLOR = new Color(153f / 255f, 153f / 255f, 153f / 255f);

        private Foldout _foldout;

        public OrganizationalFoldableContainer(string containerName, string foldoutText)
        {
            name = containerName;
            style.borderTopColor = ORGANIZATIONAL_FOLDABLE_CONTAINER_BORDER_COLOR;
            style.borderTopWidth = 1;

            _foldout = new Foldout() { text = foldoutText };
            _foldout.value = false;

            Add(_foldout);
        }

        public void SetControllers(IEnumerable<MainToolbarElementController> controllers)
        {
            _foldout.Clear();

            foreach (var controller in controllers)
            {
                _foldout.Add(controller);
            }
        }
    }
}