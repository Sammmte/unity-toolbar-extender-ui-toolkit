﻿using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public static class UnityNativeElementsIds
    {
        // ------------------- ELEMENTS TYPES ---------------------

        // LEFT
        private const string ACCOUNT_DROPDOWN_TYPE_NAME = "AccountDropdown";
        private const string CLOUD_BUTTON_TYPE_NAME = "CloudButton";
        private const string VERSION_CONTROL_BUTTON_TYPE_NAME = "MainToolbarImguiContainer";

        // RIGHT
        private const string LAYOUT_DROPDOWN_TYPE_NAME = "LayoutDropdown";
        private const string LAYERS_DROPDOWN_TYPE_NAME = "LayersDropdown";
        private const string SEARCH_BUTTON_TYPE_NAME = "SearchButton";
        private const string MODES_DROPDOWN_TYPE_NAME = "ModesDropdown";
        private const string PREVIEW_PACKAGES_IN_USE_DROPDOWN_TYPE_NAME = "PreviewPackagesInUseDropdown";
        private const string UNDO_BUTTON_TYPE_NAME = "UndoButton";

        // ------------------- ELEMENTS NAMES ---------------------

        // LEFT
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
        private const string ACCOUNT_DROPDOWN_ID = "AccountDropdown";
        private const string CLOUD_BUTTON_ID = "CloudButton";
        private const string VERSION_CONTROL_ID = "VersionControlButton";

        // CENTER
        private const string PLAY_BUTTON_ID = "PlayButton";
        private const string PAUSE_BUTTON_ID = "PauseButton";
        private const string FRAME_STEP_BUTTON_ID = "FrameStepButton";

        // RIGHT
        private const string LAYOUT_DROPDOWN_ID = "LayoutDropdown";
        private const string LAYERS_DROPDOWN_ID = "LayersDropdown";
        private const string SEARCH_BUTTON_ID = "SearchButton";
        private const string MODES_DROPDOWN_ID = "ModesDropdown";
        private const string PREVIEW_PACKAGES_IN_USE_DROPDOWN_ID = "PreviewPackagesInUseDropdown";
        private const string UNDO_BUTTON_ID = "UndoButton";

        private static readonly Dictionary<string, string> IDS_BY_TYPE = new Dictionary<string, string>()
        {
            // LEFT
            { ACCOUNT_DROPDOWN_TYPE_NAME, ACCOUNT_DROPDOWN_ID },
            { CLOUD_BUTTON_TYPE_NAME, CLOUD_BUTTON_ID },
            { VERSION_CONTROL_BUTTON_TYPE_NAME, VERSION_CONTROL_ID },

            // RIGHT
            { LAYOUT_DROPDOWN_TYPE_NAME, LAYOUT_DROPDOWN_ID },
            { LAYERS_DROPDOWN_TYPE_NAME, LAYERS_DROPDOWN_ID },
            { SEARCH_BUTTON_TYPE_NAME, SEARCH_BUTTON_ID },
            { MODES_DROPDOWN_TYPE_NAME, MODES_DROPDOWN_ID },
            { PREVIEW_PACKAGES_IN_USE_DROPDOWN_TYPE_NAME, PREVIEW_PACKAGES_IN_USE_DROPDOWN_ID },
            { UNDO_BUTTON_TYPE_NAME, UNDO_BUTTON_ID },
        };

        private static readonly Dictionary<string, string> IDS_BY_NAME = new Dictionary<string, string>()
        {
            // LEFT
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