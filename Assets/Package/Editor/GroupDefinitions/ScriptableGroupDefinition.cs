using UnityEngine;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    [CreateAssetMenu(menuName = ToolInfo.EDITOR_MENU_BASE + "/Group Definition")]
    public sealed class ScriptableGroupDefinition : ScriptableObject
    {
        [SerializeField] private string _groupId;
        [SerializeField] private string _groupName;
        [SerializeField] private ToolbarAlign _alignment;
        [SerializeField] private int _order;
        [SerializeField] [MainToolbarElementDropdown] private string[] _toolbarElementsIds;

        public string GroupId => _groupId;
        public string GroupName => _groupName;
        public ToolbarAlign Alignment => _alignment;
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