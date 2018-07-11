using UnityEditor;
using UnityEngine;

namespace EditorKit
{
    [CustomPropertyDrawer(typeof(Vector3Bool))]
    public class Vector3BoolDrawer : PropertyDrawer
    {
        /// <summary>
        /// Class for actual `Vector3Bool` references.
        /// </summary>
        private class Vector3BoolProperty
        {
            public SerializedProperty xProp;
            public SerializedProperty yProp;
            public SerializedProperty zProp;

            public string xName;
            public string yName;
            public string zName;

            public Vector3BoolProperty(SerializedProperty vector3BoolProp)
            {
                xProp = vector3BoolProp.FindPropertyRelative(nameof(Vector3Bool.x));
                yProp = vector3BoolProp.FindPropertyRelative(nameof(Vector3Bool.y));
                zProp = vector3BoolProp.FindPropertyRelative(nameof(Vector3Bool.z));
                
                xName = vector3BoolProp.FindPropertyRelative(nameof(Vector3Bool.xName)).stringValue;
                yName = vector3BoolProp.FindPropertyRelative(nameof(Vector3Bool.yName)).stringValue;
                zName = vector3BoolProp.FindPropertyRelative(nameof(Vector3Bool.zName)).stringValue;
            }
        }

        private bool firstRun = true;
        private float maxBoolWidth;
        private Vector3BoolProperty vec3BoolProp;
        private const float padding = 5.0f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (firstRun)
            {
                vec3BoolProp = new Vector3BoolProperty(property);
                firstRun = false;
            }

            EditorGUI.LabelField(position, label);
            position.xMin += EditorGUIUtility.labelWidth;
            maxBoolWidth = position.width / 3;

            EditorGUI.showMixedValue = property.hasMultipleDifferentValues;
            position.width =  maxBoolWidth + EditorKitGUI.toggleWidth - padding;
            DrawNextBoolComponent(ref position, ref vec3BoolProp.xProp, vec3BoolProp.xName);
            DrawNextBoolComponent(ref position, ref vec3BoolProp.yProp, vec3BoolProp.yName);
            DrawNextBoolComponent(ref position, ref vec3BoolProp.zProp, vec3BoolProp.zName);
            EditorGUI.showMixedValue = false;
        }

        private void DrawNextBoolComponent(ref Rect position, ref SerializedProperty boolProp, string name)
        {
            boolProp.boolValue = EditorKitGUI.ToggleRight(position, name, boolProp.boolValue);
            position.xMin += position.width;
            position.width = maxBoolWidth - padding;

            // old implementation (WIP)
            //var textWidth = ToolkitGUI.CalcTextWidth(name);
            //position.width = textWidth < maxBoolWidth ? textWidth : maxBoolWidth;
            //EditorGUI.LabelField(position, new GUIContent(name));

            //position.xMin += position.width;  // move to end of label
            //position.width = ToolkitGUI.toggleWidth;
            //boolProp.boolValue = EditorGUI.Toggle(position, boolProp.boolValue);
            //position.xMin += ToolkitGUI.toggleWidth;  // prep for next call
        }
    }
}
