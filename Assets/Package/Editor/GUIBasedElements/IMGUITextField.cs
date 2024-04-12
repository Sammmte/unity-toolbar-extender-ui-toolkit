using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public class IMGUITextField : IMGUIContainer
    {
        public event Action<string> OnValueChanged;

        private string _value;

        public string Label { get; set; }
        public string Value
        {
            get => _value;
            set
            {
                var previousValue = _value;
                _value = value;

                if (previousValue != _value)
                    OnValueChanged?.Invoke(_value);
            }
        }

        public float FieldWidth { get; set; }

        public IMGUITextField(string label, string initialValue)
        {
            Label = label;
            Value = initialValue;
            onGUIHandler = OnGUI;
        }

        public IMGUITextField(string label) : this(label, "")
        {

        }

        public IMGUITextField() : this(null, "")
        {

        }

        private void OnGUI()
        {
            GUILayout.BeginHorizontal();

            if (!string.IsNullOrEmpty(Label))
                GUILayout.Label(Label, GUILayout.ExpandWidth(false));

            Value = EditorGUILayout.DelayedTextField(Value, GUILayout.Width(FieldWidth));

            GUILayout.EndHorizontal();
        }
    }
}
