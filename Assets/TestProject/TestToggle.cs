using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor;
using UnityEngine.UIElements;

[MainToolbarElement(group: "Left Test Group")]
public class TestToggle : Toggle
{
    private const string TEST_TOGGLE_SAVE_KEY = "TestToggle";

    public TestToggle() : base("Test Toggle")
    {
        value = EditorPrefs.GetBool(TEST_TOGGLE_SAVE_KEY, false);

        this.RegisterValueChangedCallback(Save);
    }

    private void Save(ChangeEvent<bool> changeEvent)
    {
        EditorPrefs.SetBool(TEST_TOGGLE_SAVE_KEY, changeEvent.newValue);
    }
}