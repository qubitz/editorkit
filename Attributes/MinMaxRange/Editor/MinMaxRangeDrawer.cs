using UnityEngine;
using UnityEditor;

namespace EditorKit
{
    [CustomPropertyDrawer(typeof(MinMaxRangeAttribute))]
    class MinMaxSliderDrawer : PropertyDrawer
    {
        private MinMaxRangeAttribute sliderAttr = null;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.Vector2)
            {
                float textFieldWidth = 30.0f;
                sliderAttr = sliderAttr ?? attribute as MinMaxRangeAttribute;

                EditorGUI.LabelField(position, label);
                Vector2 range = property.vector2Value;
                float min = range.x;
                float max = range.y;

                Rect sliderPos = position;
                sliderPos.xMin += EditorGUIUtility.labelWidth + textFieldWidth;
                sliderPos.width -= textFieldWidth;

                EditorGUI.BeginChangeCheck();
                EditorGUI.MinMaxSlider(sliderPos, ref min, ref max, sliderAttr.min, sliderAttr.max);
                if (EditorGUI.EndChangeCheck())
                {
                    range.x = min;
                    range.y = max;
                    range = Clamp(range, sliderAttr.min, sliderAttr.max);
                    property.vector2Value = range;
                }
                EditorGUI.LabelField(position, "");

                Rect minPos = position;
                minPos.x += EditorGUIUtility.labelWidth;
                minPos.width = textFieldWidth;
                EditorGUI.LabelField(minPos, min.ToString("0.00"));
                Rect maxPos = position;
                maxPos.x += maxPos.width - textFieldWidth;
                maxPos.width = textFieldWidth;
                EditorGUI.LabelField(maxPos, max.ToString("0.00"));
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Use only with Vector2");
            }
        }

        private Vector2 Clamp(Vector2 value, float min, float max)
        {
            Vector2 newValue;
            newValue.x = Mathf.Clamp(value.x, min, max);
            newValue.y = Mathf.Clamp(value.y, min, max);
            return newValue;
        }
    }
}
