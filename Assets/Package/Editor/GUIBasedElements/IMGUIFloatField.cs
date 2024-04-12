using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public class IMGUIFloatField : IMGUIContainer
    {
        public event Action<float> OnValueChanged;

        private float _value;

        public string Label { get; set; }
        public float Value
        {
            get => _value;
            set
            {
                var previousValue = _value;
                _value = value;

                if(previousValue != _value)
                    OnValueChanged?.Invoke(_value);
            }
        }

        public float FieldWidth { get; set; }

        public IMGUIFloatField(string label, float initialValue)
        {
            Label = label;
            Value = initialValue;
            onGUIHandler = OnGUI;
        }

        public IMGUIFloatField(string label) : this(label, 0)
        {

        }

        public IMGUIFloatField(float initialValue) : this(null, initialValue)
        {

        }

        public IMGUIFloatField() : this(null, 0)
        {

        }

        private void OnGUI()
        {
            GUILayout.BeginHorizontal();

            if (!string.IsNullOrEmpty(Label))
                GUILayout.Label(Label, GUILayout.ExpandWidth(false));

            Value = EditorGUILayout.DelayedFloatField(Value, GUILayout.Width(FieldWidth));

            GUILayout.EndHorizontal();
        }
    }
}
