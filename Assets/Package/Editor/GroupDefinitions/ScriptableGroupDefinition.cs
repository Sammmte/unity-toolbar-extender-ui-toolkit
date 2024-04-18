using UnityEngine;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    [CreateAssetMenu(menuName = ToolInfo.EDITOR_MENU_BASE + "/Group Definition")]
    public sealed class ScriptableGroupDefinition : ScriptableObject
    {
        [SerializeField] private string _groupId;
        [SerializeField] private string _groupName;
        [SerializeField] [ParentGroupIdDropdown] private string _parentGroupId;
        [SerializeField] private ToolbarAlign _alignment;
        [SerializeField] private int _order;
        [SerializeField] [MainToolbarElementDropdown] private string[] _toolbarElementsIds;

        public string GroupId => _groupId;
        public string GroupName => _groupName;
        public string ParentGroupId => _parentGroupId;
        public ToolbarAlign Alignment => _alignment;
        public int Order => _order;
        public string[] ToolbarElementsIds => _toolbarElementsIds == null ? new string[0] : _toolbarElementsIds;
    }
}