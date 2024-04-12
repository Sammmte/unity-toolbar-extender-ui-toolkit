using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public class IMGUIIntField : IMGUIContainer
    {
        public event Action<int> OnValueChanged;

        private int _value;

        public string Label { get; set; }
        public int Value
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

        public IMGUIIntField(string label, int initialValue)
        {
            Label = label;
            Value = initialValue;
            onGUIHandler = OnGUI;
        }

        public IMGUIIntField(string label) : this(label, 0)
        {

        }

        public IMGUIIntField(int initialValue) : this(null, initialValue)
        {

        }

        public IMGUIIntField() : this(null, 0)
        {

        }

        private void OnGUI()
        {
            GUILayout.BeginHorizontal();

            if (!string.IsNullOrEmpty(Label))
                GUILayout.Label(Label, GUILayout.ExpandWidth(false));

            Value = EditorGUILayout.DelayedIntField(Value, GUILayout.Width(FieldWidth));

            GUILayout.EndHorizontal();
        }
    }
}
