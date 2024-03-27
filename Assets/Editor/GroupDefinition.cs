using UnityEngine;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    [CreateAssetMenu(menuName = ToolInfo.EDITOR_MENU_BASE + "/Group Definition")]
    public sealed class GroupDefinition : ScriptableObject
    {
        [SerializeField] private string _groupName;
        [SerializeField] private ToolbarAlign _alignment;
        [SerializeField] private int _order;
        [SerializeField] [MainToolbarElementTypeDropdown] private string[] _toolbarElementsTypes;

        public string GroupName => _groupName;
        public ToolbarAlign Alignment => _alignment;
        public int Order => _order;
        public string[] ToolbarElementsTypes => _toolbarElementsTypes;
    }
}