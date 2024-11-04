using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal static class UnityNativeElementsIds
    {
        // ------------------- ELEMENTS TYPES ---------------------

        // LEFT
        private const string ACCOUNT_DROPDOWN_TYPE_NAME = "AccountDropdown";
        private const string CLOUD_BUTTON_TYPE_NAME = "CloudButton";
        private const string VERSION_CONTROL_BUTTON_TYPE_NAME = "MainToolbarImguiContainer";
        private const string STORE_BUTTON_TYPE_NAME = "StoreButton";

        // RIGHT
        private const string LAYOUT_DROPDOWN_TYPE_NAME = "LayoutDropdown";
        private const string LAYERS_DROPDOWN_TYPE_NAME = "LayersDropdown";
        private const string SEARCH_BUTTON_TYPE_NAME = "SearchButton";
        private const string MODES_DROPDOWN_TYPE_NAME = "ModesDropdown";
        private const string PREVIEW_PACKAGES_IN_USE_DROPDOWN_TYPE_NAME = "PreviewPackagesInUseDropdown";
        private const string UNDO_BUTTON_TYPE_NAME = "UndoButton";
        private const string MULTIPLAYER_ROLE_DROPDOWN_TYPE_NAME = "MultiplayerRoleDropdown";

        // ------------------- ELEMENTS NAMES ---------------------

        // LEFT
        private const string TOOLBAR_PRODUCT_CAPTION_NAME = "ToolbarProductCaption";
        private const string ACCOUNT_DROPDOWN_ELEMENT_NAME = "AccountDropdown";
        private const string CLOUD_BUTTON_ELEMENT_NAME = "Cloud";

        // CENTER
        private const string PLAY_BUTTON_ELEMENT_NAME = "Play";
        private const string PAUSE_BUTTON_ELEMENT_NAME = "Pause";
        private const string FRAME_STEP_BUTTON_ELEMENT_NAME = "Step";

        // RIGHT
        private const string LAYOUT_DROPDOWN_ELEMENT_NAME = "LayoutDropdown";
        private const string LAYERS_DROPDOWN_ELEMENT_NAME = "LayersDropdown";
        private const string MODES_DROPDOWN_ELEMENT_NAME = "ModesDropdown";
        private const string PREVIEW_PACKAGES_IN_USE_DROPDOWN_ELEMENT_NAME = "PreviewPackagesInUseDropdown";
        private const string UNDO_BUTTON_ELEMENT_NAME = "History";

        // ------------------- FIXED IDS ---------------------

        // LEFT
        public const string TOOLBAR_PRODUCT_CAPTION = "ToolbarProductCaption";
        public const string ACCOUNT_DROPDOWN_ID = "AccountDropdown";
        public const string CLOUD_BUTTON_ID = "CloudButton";
        public const string VERSION_CONTROL_ID = "VersionControlButton";
        public const string STORE_BUTTON_ID = "StoreButton";

        // CENTER
        public const string PLAY_BUTTON_ID = "PlayButton";
        public const string PAUSE_BUTTON_ID = "PauseButton";
        public const string FRAME_STEP_BUTTON_ID = "FrameStepButton";

        // RIGHT
        public const string LAYOUT_DROPDOWN_ID = "LayoutDropdown";
        public const string LAYERS_DROPDOWN_ID = "LayersDropdown";
        public const string SEARCH_BUTTON_ID = "SearchButton";
        public const string MODES_DROPDOWN_ID = "ModesDropdown";
        public const string PREVIEW_PACKAGES_IN_USE_DROPDOWN_ID = "PreviewPackagesInUseDropdown";
        public const string UNDO_BUTTON_ID = "UndoButton";
        public const string MULTIPLAYER_ROLE_DROPDOWN = "MultiplayerRoleDropdown";

        private static readonly Dictionary<string, string> IDS_BY_TYPE = new Dictionary<string, string>()
        {
            // LEFT
            { ACCOUNT_DROPDOWN_TYPE_NAME, ACCOUNT_DROPDOWN_ID },
            { CLOUD_BUTTON_TYPE_NAME, CLOUD_BUTTON_ID },
            { VERSION_CONTROL_BUTTON_TYPE_NAME, VERSION_CONTROL_ID },
            { STORE_BUTTON_TYPE_NAME, STORE_BUTTON_ID },

            // RIGHT
            { LAYOUT_DROPDOWN_TYPE_NAME, LAYOUT_DROPDOWN_ID },
            { LAYERS_DROPDOWN_TYPE_NAME, LAYERS_DROPDOWN_ID },
            { SEARCH_BUTTON_TYPE_NAME, SEARCH_BUTTON_ID },
            { MODES_DROPDOWN_TYPE_NAME, MODES_DROPDOWN_ID },
            { PREVIEW_PACKAGES_IN_USE_DROPDOWN_TYPE_NAME, PREVIEW_PACKAGES_IN_USE_DROPDOWN_ID },
            { UNDO_BUTTON_TYPE_NAME, UNDO_BUTTON_ID },
            { MULTIPLAYER_ROLE_DROPDOWN_TYPE_NAME, MULTIPLAYER_ROLE_DROPDOWN }
        };

        private static readonly Dictionary<string, string> IDS_BY_NAME = new Dictionary<string, string>()
        {
            // LEFT
            { TOOLBAR_PRODUCT_CAPTION_NAME, TOOLBAR_PRODUCT_CAPTION },
            { ACCOUNT_DROPDOWN_ELEMENT_NAME, ACCOUNT_DROPDOWN_ID },
            { CLOUD_BUTTON_ELEMENT_NAME, CLOUD_BUTTON_ID },

            // CENTER
            { PLAY_BUTTON_ELEMENT_NAME, PLAY_BUTTON_ID },
            { PAUSE_BUTTON_ELEMENT_NAME, PAUSE_BUTTON_ID },
            { FRAME_STEP_BUTTON_ELEMENT_NAME, FRAME_STEP_BUTTON_ID },

            // RIGHT
            { LAYOUT_DROPDOWN_ELEMENT_NAME, LAYOUT_DROPDOWN_ID },
            { LAYERS_DROPDOWN_ELEMENT_NAME, LAYERS_DROPDOWN_ID },
            { MODES_DROPDOWN_ELEMENT_NAME, MODES_DROPDOWN_ID },
            { PREVIEW_PACKAGES_IN_USE_DROPDOWN_ELEMENT_NAME, PREVIEW_PACKAGES_IN_USE_DROPDOWN_ID },
            { UNDO_BUTTON_ELEMENT_NAME, UNDO_BUTTON_ID },
        };

        public static string IdOf(VisualElement visualElement)
        {
            var typeName = visualElement.GetType().Name;

            if(IDS_BY_TYPE.ContainsKey(typeName))
                return IDS_BY_TYPE[typeName];

            var elementName = visualElement.name;

            if(IDS_BY_NAME.ContainsKey(elementName))
                return IDS_BY_NAME[elementName];

            return null;
        }
    }
}