using System;
using UnityEngine;
using UnityEditor;

namespace EditorKit
{
    [CustomPropertyDrawer(typeof(OptionalAttribute))]
    public class OptionalDrawer : PropertyDrawer
    {
        private OptionalAttribute attr;
        private SerializedProperty property;
        private GUIContent label;
        private bool firstRun = true;
        private bool inferredToggle = false;

        private const float toggleWidth = 20.0f;
        private const float propertyPadding = 10.0f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (firstRun)
            {
                attr = attribute as OptionalAttribute;
                this.property = property;
                this.label = label;
            }

            if (attr.boolPath == String.Empty)
            {
                if (property.propertyType == SerializedPropertyType.ObjectReference)
                    DrawInferredOptionalField(position);
                else
                    EditorGUI.LabelField(position, label.text, "Inferred optional only valid for object references");
            }
            else
            {
                var boolProp = property.serializedObject.FindProperty(attr.boolPath);
                if (boolProp == null)
                {
                    EditorGUI.LabelField(position, label.text, $"Cannot find referenced boolean \"{attr.boolPath}\"");
                }
                else
                {
                    if (boolProp.propertyType == SerializedPropertyType.Boolean)
                    {
                        bool boolValue = boolProp.boolValue;
                        DrawOptionalField(ref boolValue, position);
                        boolProp.boolValue = boolValue;
                    }
                    else
                    {
                        EditorGUI.LabelField(position, label.text, $"Referenced type must be boolean \"{attr.boolPath}\"");
                    }
                }
            }

            firstRun = false;
        }

        private void DrawInferredOptionalField(Rect position)
        {
            bool prev = inferredToggle;
            inferredToggle = !inferredToggle || (property.objectReferenceValue != null);
            bool curr = inferredToggle;

            if (!firstRun && prev == false && curr == true && property.objectReferenceValue != null)
                Debug.LogWarning("Editor Toolkit: Must delete object reference to uncheck optional property.");

            DrawOptionalField(ref inferredToggle, position);
        }

        private void DrawOptionalField(ref bool shouldDrawProperty, Rect position)
        {
            bool shouldDrawAsNewLine = (attr.type == OptionalAttribute.Format.NewLine);

            Rect togglePos = position;
            togglePos.width = EditorGUIUtility.labelWidth + toggleWidth;
            shouldDrawProperty = EditorKitGUI.ToggleAlignRight(togglePos, label, shouldDrawProperty);

            if (shouldDrawProperty)
            {
                if (shouldDrawAsNewLine)
                    DrawNewlinePropertyField(position);
                else
                    DrawInlinePropertyField(position);
            }
        }

        private void DrawInlinePropertyField(Rect position)
        {
            position.xMin = EditorGUIUtility.labelWidth;
                        
            if (IsIntOrFloat(property))
                position.xMin += propertyPadding;  // allow for click-n-drag

            DrawProperty(position);
        }

        private void DrawNewlinePropertyField(Rect position)
        {
            position.y += EditorGUIUtility.singleLineHeight;

            // Reserve vertical space
            GUILayout.BeginVertical();
            GUILayout.Space(EditorGUI.GetPropertyHeight(property));
            GUILayout.EndVertical();

            position.xMin += EditorGUIUtility.labelWidth;
            DrawProperty(position);
        }

        private void DrawProperty(Rect position)
        {
            var content = IsIntOrFloat(property) ? new GUIContent(" ") : GUIContent.none;

            float prevWidth = EditorGUIUtility.labelWidth;
            if (!property.hasChildren) 
                EditorGUIUtility.labelWidth = toggleWidth;
            EditorGUI.PropertyField(position, property, content, true);
            EditorGUIUtility.labelWidth = prevWidth;
        }

        private bool IsIntOrFloat(SerializedProperty property)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                case SerializedPropertyType.Float:
                    return true;
            }
            return false;
        }
    }

}
