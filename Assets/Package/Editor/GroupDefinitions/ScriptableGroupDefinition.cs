using UnityEngine;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    [CreateAssetMenu(menuName = ToolInfo.EDITOR_MENU_BASE + "/Group Definition")]
    public sealed class ScriptableGroupDefinition : ScriptableObject
    {
        [SerializeField]
        [Tooltip("Id of group. It must be unique")]
        private string _groupId;
        [SerializeField]
        [Tooltip("Text to display in group element dropdown")]
        private string _groupName;
        [Tooltip("Order when used as root element. Ignored inside other groups. Order of elements inside a group is determined by ToolbarElementsIds array elements order.")]
        [SerializeField] private int _order;
        [SerializeField]
        [Tooltip("Elements ids of this group. Order of elements in array determines the order in which the elements will be displayed")]
        [MainToolbarElementDropdown] private string[] _toolbarElementsIds;

        public string GroupId => _groupId;
        public string GroupName => _groupName;
        public int Order => _order;
        public string[] ToolbarElementsIds => _toolbarElementsIds == null ? new string[0] : _toolbarElementsIds;

        private void OnValidate()
        {
            SetEqualValuesToEmptyInOrder();
        }

        private void SetEqualValuesToEmptyInOrder()
        {
            for (int i = 0; i < _toolbarElementsIds.Length; i++)
            {
                var value = _toolbarElementsIds[i];
                for (int j = 0; j < _toolbarElementsIds.Length; j++)
                {
                    if (i == j)
                        continue;

                    if (_toolbarElementsIds[j] == value)
                    {
                        _toolbarElementsIds[j] = "";
                    }
                }
            }
        }
    }
}