using System;
using UnityEditor;
using UnityEngine;

namespace HephaestusForge.FlaggedEnum
{
    [CustomPropertyDrawer(typeof(Enum), true)]
    public class EnumPropertyDrawer : PropertyDrawer
    {
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
