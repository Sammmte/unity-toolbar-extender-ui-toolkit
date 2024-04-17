using UnityEngine;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    [CreateAssetMenu(menuName = ToolInfo.EDITOR_MENU_BASE + "/Group Definition")]
    public sealed class ScriptableGroupDefinition : ScriptableObject
    {
        [SerializeField] private string _groupId;
        [SerializeField] private ToolbarAlign _alignment;
        [SerializeField] private int _order;
        [SerializeField] [MainToolbarElementDropdown] private string[] _toolbarElementsIds;

        public string GroupId => _groupId;
        public ToolbarAlign Alignment => _alignment;
        public int Order => _order;
        public string[] ToolbarElementsIds => _toolbarElementsIds;
    }
}