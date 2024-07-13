using UnityEngine;
using UnityEditor;
using MeasurementUnit;

namespace MeasurementUnits
{

    [CustomPropertyDrawer(typeof(Length))]
    public class LengthPropertyDrawer : PropertyDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Using BeginProperty / EndProperty on the parent property means that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            Rect valueRect = new(position.x, position.y, position.width - 64, position.height);
            Rect unitRect = new(position.x + position.width - 64 + EditorGUIUtility.standardVerticalSpacing, position.y, 64 - EditorGUIUtility.standardVerticalSpacing, position.height);

            SerializedProperty valueProperty = property.FindPropertyRelative("value");
            SerializedProperty unitValueProperty = property.FindPropertyRelative("unitValue");
            SerializedProperty unitProperty = property.FindPropertyRelative("unit");

            EditorGUI.BeginChangeCheck();

            EditorGUI.PropertyField(valueRect, unitValueProperty, GUIContent.none);
            EditorGUI.PropertyField(unitRect, unitProperty, GUIContent.none);

            if (EditorGUI.EndChangeCheck())
            {
                valueProperty.floatValue = (Length.Unit)unitProperty.enumValueIndex == Length.Unit.Metres
                ? unitValueProperty.floatValue
                : unitValueProperty.floatValue * Length.YARD_TO_METRE;
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

    }

}
