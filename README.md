# Unity Toolbar Extender UI Toolkit

Inspired on marijnz's great [Unity Toolbar Extender](https://github.com/marijnz/unity-toolbar-extender), Unity Toolbar Extender UI Toolkit allows you to extend Unity's main toolbar using Unity's UI Toolkit.

# Important notes on Unity 6.3 LTS new main toolbar customization API ![](Assets/Package/Readme-Resources~/warning.png)

Unity 6.3 LTS added a [new feature](https://discussions.unity.com/t/upcoming-feature-highlight-main-toolbar-customization/1490256) to create your own custom main toolbar elements. If you're working with any previous version, use version 2.0.0 of this package. If you're on Unity 6.3 LTS you need version 3.0.0 of this package.

### Why use this toolbar extender plugin if Unity has it's own solution?

Currently Unity's API is lacking the ability of customizing the actual VisualElement you're working with. They provided a simple API to create basic elements, like buttons, sliders, dropdowns, etc. but without giving us access to any VisualElement object, although they use UI Toolkit behind scenes.

Nonetheless, Unity made some really cool features on you can still use along this plugin, like drag 'n drop ordering of elements, quick hide and show feature, saving presets, etc.

You can learn more about this new API at [Unity docs](https://docs.unity3d.com/6000.3/Documentation/ScriptReference/Toolbars.MainToolbarElement.html) and at [their forums](https://discussions.unity.com/t/upcoming-feature-highlight-main-toolbar-customization/1490256)

# Install

## via npm

Open `Packages/manifest.json` with your favorite text editor. Add a [scoped registry](https://docs.unity3d.com/Manual/upm-scoped.html) and following line to dependencies block:
```json
{
  "scopedRegistries": [
    {
      "name": "npmjs",
      "url": "https://registry.npmjs.org/",
      "scopes": [
        "com.paps"
      ]
    }
  ],
  "dependencies": {
    "com.paps.unity-toolbar-extender-ui-toolkit": "1.0.0"
  }
}
```
Package should now appear in package manager.

## via OpenUPM

The package is also available on the [openupm registry](https://openupm.com/packages/com.paps.unity-toolbar-extender-ui-toolkit). You can install it eg. via [openupm-cli](https://github.com/openupm/openupm-cli).

```
openupm add com.paps.unity-toolbar-extender-ui-toolkit
```

## via Git URL

Open Package Manager window, Go to `Top left plus icon -> Add package from git URL`. There add the following: `https://github.com/Sammmte/unity-toolbar-extender-ui-toolkit.git?path=/Assets/Package`

# Getting Started

It is recommended to import package samples in your project. To do this `Open Package Manager -> Filter by In Project packages -> Click Unity Toolbar Extender UI Toolkit package -> In package window select Samples -> Click Import`.

## Create Common Controls

Inherit from VisualElement, tag it with `MainToolbarElementAttribute` and define an id. Then you need to define a "dummy" `MainToolbarElement` using Unity's new API. This dummy will be null but will be tagged with Unity's `MainToolbarElementAttribute` and MUST have the id defined in Paps attribute. 

You can place the script in any `Editor` folder or subfolder or in a folder with an editor assembly definition.

```csharp
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;
using Paps.UnityToolbarExtenderUIToolkit;

[Paps.UnityToolbarExtenderUIToolkit.MainToolbarElement("MyAwesomeButton")]
public class AwesomeButton : Button
{
    // Ids MUST be equal!
    // You can use defaultDockPosition and defaultDockIndex 
    // to choose where your element will be in the toolbar
    [UnityEditor.Toolbars.MainToolbarElement("MyAwesomeButton", defaultDockPosition = MainToolbarDockPosition.Middle)]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummy()
    {
        // Return null here. The Toolbar Extender will replace this with yours
        return null;
    }

    public void InitializeElement()
    {
        text = "Awesome Button";
        clicked += () =>
        {
            Debug.Log("This is an awesome button, indeed!");
        };
    }
}
```

![](Assets/Package/Readme-Resources~/common-button-example.png)

### Any VisualElement type works

```csharp
using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine.UIElements;
using System.Collections.Generic;

[MainToolbarElement(id: "AwesomeToggle")]
public class MyAwesomeToggle : Toggle
{
    [UnityEditor.Toolbars.MainToolbarElement("AwesomeToggle")]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummy()
    {
        return null;
    }
    
    public void InitializeElement()
    {
        label = "Awesome Toggle";
    }
}

[MainToolbarElement(id: "AwesomeDropdownField")]
public class MyAwesomeDropdownField : DropdownField
{
    [UnityEditor.Toolbars.MainToolbarElement("AwesomeDropdownField")]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummy()
    {
        return null;
    }
    
    public void InitializeElement()
    {
        label = "Awesome Dropdown";
        choices = new List<string>() { "Option 1", "Option 2"};
    }
}

[MainToolbarElement(id: "AwesomeSlider")]
public class MyAwesomeSlider : Slider
{
    [UnityEditor.Toolbars.MainToolbarElement("AwesomeSlider")]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummy()
    {
        return null;
    }
    
    public void InitializeElement()
    {
        label = "Awesome Slider";
        lowValue = 0;
        highValue = 100;
    }
}

[MainToolbarElement("MyAwesomeIntField")]
public class MyAwesomeIntegerField : IntegerField
{
    [UnityEditor.Toolbars.MainToolbarElement("MyAwesomeIntField")]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummy()
    {
        return null;
    }
    
    public void InitializeElement()
    {
        label = "Awesome Int";
    }
}
```

![](Assets/Package/Readme-Resources~/other-common-controls.png)

## Important Notes ![](Assets/Package/Readme-Resources~/warning.png)

- If you don't have a Unity MainToolbarElement counterpart or the ids don't match, your visual elements will be placed inside a group dropdown that groups any "orphan" element. This dropdown has a text `"Orphan Elements"`
- If you want to apply custom style to your elements, please read [Styling Your Main Toolbar Elements](#styling-your-main-toolbar-elements) section.
- You can define a main toolbar element that inherits from `IMGUIContainer` to render stuff with `IMGUI`. Remember to use `GUILayout.BeginHorizontal` and `GUILayout.EndHorizontal` to render your things in row.
- Main toolbar elements may be instantiated more than once, whenever `MainToolbarAutomaticExtender` refreshes. Take it into account if you need to make some heavy process with your elements.

# Prevent data loss upon entering Play mode or closing the editor

Mark a field or property with the `SerializeAttribute` to save its value whenever it changes. Otherwise if you enter Play mode or close the editor data will be reset.

```csharp
using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine.UIElements;

[MainToolbarElement("AwesomeInt")]
public class MyAwesomeInt : IntegerField
{
    [UnityEditor.Toolbars.MainToolbarElement("AwesomeInt")]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummy()
    {
        return null;
    }
    
    [Serialize] private int _intValue;

    public void InitializeElement()
    {
        value = _intValue; // set saved value to integer field value
        label = "Awesome Int";

        RegisterCallback<ChangeEvent<int>>(ev =>
        {
            _intValue = ev.newValue;
        });
    }
}
```

Fields and properties are serialized with their names by default.

You can set your own serialization key to prevent data loss when a field or property name changes due to, for example, refactors.

```csharp
using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine.UIElements;

[MainToolbarElement("AwesomeInt")]
public class MyAwesomeInt : IntegerField
{
    [UnityEditor.Toolbars.MainToolbarElement("AwesomeInt")]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummy()
    {
        return null;
    }
    
    [Serialize("fixed-int-name")] private int _intValue;

    public void InitializeElement()
    {
        value = _intValue; // set saved value to integer field value
        label = "Awesome Int";

        RegisterCallback<ChangeEvent<int>>(ev =>
        {
            _intValue = ev.newValue;
        });
    }
}
```

## Important Notes ![](Assets/Package/Readme-Resources~/warning.png)

- Use `InitializeElement` method to access restored values. Don't use constructors because the system injects the serialized values once the object is created.
- To serialize values this tool uses [Unity Serialization Package](https://docs.unity3d.com/Packages/com.unity.serialization@3.1/manual/index.html), therefore you can use its features, like `JsonAdapters` to serialize your objects the way you want. You can add a custom `JsonAdapter` from a method with `InitializeOnLoadMethod`, then add a global `JsonAdapter` (check the docs).
- You can serialize anything `Unity Serialization Package` serializes, including dictionaries, custom classes and structs, Unity objects like GameObject, any component, etc.

# Group Your Elements to Save Space

Create a **Group Definition** asset and group any visual element in it. They will be hidden and shown by a dropdown.

To create a **Group Definition** asset go to `Project Window -> Right Click -> Create -> Paps -> Unity Toolbar Extender UI Toolkit -> Group Definition`. After that you need to define a `MainToolbarElement` from Unity's new API with the same id as the group for it to be placed in the toolbar. Otherwise it will be added to the orphan elements group.

```csharp
public static class ExampleGroupsProvider
{
    [UnityEditor.Toolbars.MainToolbarElement("MyRootGroup")]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummyGroup()
    {
        // Return null here
        return null;
    }
    
    [UnityEditor.Toolbars.MainToolbarElement("MyRootGroup2")]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummyGroup2()
    {
        // Return null here
        return null;
    }
}
```

![](Assets/Package/Readme-Resources~/group-demonstration.gif)

## Make Subgroups

When configuring `ToolbarElementsIds` property in your group definition asset, add a new element and select the id of other group asset.

![](Assets/Package/Readme-Resources~/subgroup-demonstration.gif)

## Allow Subwindows

Group elements display a small window that works almost like a popup window. If your custom element displays a window, by default the group element window will be closed. To tell group element window that your window is a subwindow mark your `EditorWindow` derived class with  `GroupPopupSubWindowAttribute`. This attribute can also be used with classes derived from `PopupWindowContent`.

Example with `EditorWindow`

```csharp
using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement("MyDropdownWithWindow")]
public class DropdownInGroupWithWindow : EditorToolbarDropdown
{
    public DropdownInGroupWithWindow()
    {
        text = nameof(DropdownInGroupWithWindow);
        clicked += ShowDropdownWindow;
    }

    private void ShowDropdownWindow()
    {
        var newWindowDropdown = ScriptableObject.CreateInstance<DropdownWindow>();
        // Check important notes to know more about ShowAsDropdownForMainToolbar method
        newWindowDropdown.ShowAsDropdownForMainToolbar(worldBound, new Vector2(200, 200));
    }
}

[GroupPopupSubWindow]
public class DropdownWindow : EditorWindow
{
    private void CreateGUI()
    {
        var debugButton = new Button(() => Debug.Log("What a debug!"));
        debugButton.text = "Debug";

        var closeButton = new Button(() => Close());
        closeButton.text = "Close";

        rootVisualElement.Add(debugButton);
        rootVisualElement.Add(closeButton);
    }
}
```

Example with `PopupWindowContent`

```csharp
using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement("MyDropdownWithPopupWindowContent")]
public class DropdownInGroupWithPopupWindow : EditorToolbarDropdown
{
    public DropdownInGroupWithPopupWindow()
    {
        text = nameof(DropdownInGroupWithPopupWindow);
        clicked += ShowPopupWindow;
    }

    private void ShowPopupWindow()
    {
        UnityEditor.PopupWindow.Show(worldBound, new PopupWindowContentThatWorksInASubgroup());
    }
}

[GroupPopupSubWindow]
public class PopupWindowContentThatWorksInASubgroup : PopupWindowContent
{
    public override void OnOpen()
    {
        var debugButton = new Button(() => Debug.Log("What a debug!"));
        debugButton.text = "Debug";

        var closeButton = new Button(() => editorWindow.Close());
        closeButton.text = "Close";

        editorWindow.rootVisualElement.Add(debugButton);
        editorWindow.rootVisualElement.Add(closeButton);
    }

    public override void OnGUI(Rect rect)
    {
        
    }
}
```

![](Assets/Package/Readme-Resources~/subwindow-demonstration.gif)

## Important Notes ![](Assets/Package/Readme-Resources~/warning.png)

- Groups display their inner elements in column.
- The order the inner elements are displayed is determined by the `ToolbarElementsIds` array elements order.
- Group popup windows won't close if focused window is `UI Toolkit Debugger`, so you can analyze your elements inside these windows.
- When showing a custom window as a dropdown, with `EditorWindow.ShowAsDropdown` method, it is recommended to use a extension method provided in this package, named `ShowAsDropdownForMainToolbar`. This method uses the original inside but applies some position adjustments that would, otherwise, render the window at a wrong position. For some reason this does not happen with `PopupWindow.Show` method.

# Hide your elements

Open Main Toolbar Control Panel window, go to `Tools -> Paps -> Unity Toolbar Extender UI Toolkit -> Windows -> Main Toolbar Control Panel`. There you can hide or show any custom toolbar element made with this package.

## Unity allows this too

You can hide elements when they are in the toolbar by right clicking on them and click on Hide. You can show them again by going to toolbar's rightmost 3 dots element. There search for your element by id to make it appear again.

![](Assets/Package/Readme-Resources~/main-toolbar-control-panel-demonstration.gif)

## Important Notes ![](Assets/Package/Readme-Resources~/warning.png)

- `MainToolbarAutomaticExtender` hides visual elements by setting their style property `display` to `Display.None`.
- You can hide your custom elements using Main Toolbar Control Panel window, but they must be enabled by Unity's toolbar itself (you can check at the 3 dots element). Otherwise your elements won't be visible.

# Styling Your Main Toolbar Elements

Some common built-in visual elements have some issues when added to the toolbar containers (e.g `Button` and `DropdownField` that are rendered with a white broken-like texture), and some other when they are added to the group element window (e.g `EditorToolbarDropdown` that doesn't render its foldout arrow).

Check this example.

![](Assets/Package/Readme-Resources~/broken-element-example.jpg)

To workaround this, `MainToolbarAutomaticExtender` applies some changes to style of most of your custom visual elements when they are added either to toolbar containers or to group element windows.

Because of this, if you want to style your custom visual elements yourself you should set `MainToolbarElementAttribute` parameter `useRecommendedStyles` to `false`. Otherwise, your overrides might collide with extender's overrides. You can check if your class will be styled with recommended styles in [this list](#eligible-classes-for-recommended-style-application).

```csharp
using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine.UIElements;

[MainToolbarElement(id: "StyledButton", useRecommendedStyles: false)]
public class MyStyledButton : Button
{
    [UnityEditor.Toolbars.MainToolbarElement("StyledButton", defaultDockPosition = MainToolbarDockPosition.Middle)]
    public static UnityEditor.Toolbars.MainToolbarElement CreateDummyGroup()
    {
        // Return null here
        return null;
    }
    
    public void InitializeElement()
    {
        // I change the style of my button here
    }
}
```

# Eligible Classes for Recommended Style Application

If your custom main toolbar element meets at least one of the following criteria, it will be eligible to be styled with recommended styles:

- Inherits from `Button` and does not inherits from `EditorToolbarButton`
- Inherits from `Toggle` and does not inherits from `EditorToolbarToggle`
- Inherits from `Slider`
- Inherits from `DropdownField`
- Inherits from `EditorToolbarDropdown`
- Inherits from `IntegerField`
- Inherits from `FloatField`
- Inherits from `TextField`
- Inherits from `EditorToolbarToggle`
- Inherits from `Vector2Field`
- Inherits from `Vector3Field`
- Inherits from `ColorField`
- Inherits from `LayerField`
- Inherits from `EnumField`
- Inherits from `EnumFlagsField`
- Inherits from `TagField`
- Inherits from `ObjectField`

Remember that you can disable this feature by setting `useRecommendedStyles` to `false` in `MainToolbarElementAttribute` constructor.

# Miscelaneous Information

## About MainToolbarAutomaticExtender Class:

- This is the main manager. Most of the magic happens inside.
- This class **WON'T** do anything, unless you mark at least one type derived from `VisualElement` with `MainToolbarElementAttribute`.

## About How This Package Saves Its Data

- This package saves json files serialized with [Unity Serialization Package](https://docs.unity3d.com/Packages/com.unity.serialization@3.1/manual/index.html) inside `UserSettings` folder. `UserSettings` is a folder that should not be under your version control (e.g. Git) and is a place to save project-and-user preferences. If you experience some weird behaviour and you suspect it could be this cache data, you can delete the related file to start over.

# Tested Versions

This package was tested in:

- Unity 6000.3.0f1
  - Windows

# License

MIT LICENSE
        