# Changelog

## [3.0.0]

### Added

- Support to work along Unity's new toolbar customization API

### Removed

- ToolbarAlign enum and anything related to native Unity elements

## [2.0.0]

### Added

- Main toolbar elements field and property serialization, using SerializeAttribute and calling InitializeElement method if it exists
- Recommended styles for: Vector2Field, Vector3Field, ColorField, LayerField, EnumField, EnumFlagsField, TagField and ObjectField
- Ids for native elements on Unity 6
- ObjectSelector, SearchPickerWindow and EditorGenericDropdownMenuWindowContent won't make group popup close
- Support for Mac (mostly)

### Changed

- **Breaking:** Overrides and some stats saved by control panel window are now saved under UserSettings folder
- **Breaking:** Set AutoReferenced to false on assembly definition
- **Breaking:** Added new dependency Unity serialization (while being AutoReferenced)

### Removed

- Newtonsoft Json.Net dependency
- Delete actions. Just remove UserSettings/unity-toolbar-extender-ui-toolkit/ files to remove the thing you want

## [1.0.1]

### Fixed

- Recommended styles throwing exceptions when some of them have no label element created

## [1.0.0]

### Added

- Source Code