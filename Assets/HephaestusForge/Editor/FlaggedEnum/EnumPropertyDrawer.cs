using System;
using UnityEditor;
using UnityEngine;

namespace HephaestusForge.FlaggedEnum
{
    /// <summary>
    /// A property drawer for all enums.
    /// </summary>
    [CustomPropertyDrawer(typeof(Enum), true)]
    public class EnumPropertyDrawer : PropertyDrawer
    {
        /// <summary>
        /// Method to draw the enum, if it has the flags attribute defined it will use the EditorGUI.EnumflagsField to draw,
        /// otherwise it will just draw the enum normally.
        /// Unity only expect the enum value to be int32.
        /// </summary>
        /// <param name="position">The position in the inspector.</param>
        /// <param name="property">The serialized enum, the value is saved as an int.</param>
        /// <param name="label">A label with a displayname.</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (fieldInfo.FieldType.IsDefined(typeof(FlagsAttribute), false))
            {
                var enumVal = (Enum)Enum.ToObject(fieldInfo.FieldType, property.intValue);

                property.intValue = (int)Enum.ToObject(fieldInfo.FieldType, EditorGUI.EnumFlagsField(position, label, enumVal));
            }
            else
            {
                EditorGUI.PropertyField(position, property);
            }
        }
    }
}
