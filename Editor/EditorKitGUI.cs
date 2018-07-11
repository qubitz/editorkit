using UnityEngine;
using UnityEditor;

namespace EditorKit
{
    /// <summary>
    /// Extension onto Unity's `EditorGUI`.
    /// </summary>
    public static class EditorKitGUI
    {
        public const float toggleWidth = 11.0f;

        #region Editor Toggle Variants
        public static bool ToggleRight(Rect position, string label, bool value, GUIStyle labelStyle = null)
        {
            return ToggleRight(position, new GUIContent(label), value, labelStyle);
        }

        public static bool ToggleRight(Rect position, GUIContent label, bool value, GUIStyle labelStyle = null)
        {
            labelStyle = labelStyle ?? EditorStyles.label;

            var textWidth = CalcTextWidth(label.text);
            var maxLabelWidth = position.width - toggleWidth;

            position.width = textWidth < maxLabelWidth ? textWidth : maxLabelWidth;
            EditorGUI.LabelField(position, label);

            position.xMin += position.width;
            position.xMax += toggleWidth;
            return EditorGUI.Toggle(position, value);
        }

        public static bool ToggleAlignRight(Rect position, string label, bool value, GUIStyle labelStyle = null)
        {
            return ToggleAlignRight(position, new GUIContent(label), value, labelStyle);
        }

        public static bool ToggleAlignRight(Rect position, GUIContent label, bool value, GUIStyle labelStyle = null)
        {
            labelStyle = labelStyle ?? EditorStyles.label;

            position.width = EditorGUIUtility.labelWidth - toggleWidth;
            EditorGUI.LabelField(position, label);

            position.xMin += position.width;
            position.xMax += toggleWidth;
            return EditorGUI.Toggle(position, value);
        }
        #endregion
        
        public static float CalcTextWidth(string text)
        {
            return GUI.skin.label.CalcSize(new GUIContent(text)).x;
        }
    }
}
