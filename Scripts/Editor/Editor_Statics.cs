using System;
using UnityEditor;
using UnityEngine;

namespace PUCPR.AutoRollCredits.Editor
{
    public class Editor_Statics : UnityEditor.Editor
    {
        public static void DrawGroup(string label, Action groupContent)
        {
            DrawLabel(label);
            groupContent();
            EditorGUILayout.Space(10);
        }

        public static void DrawProperty(SerializedObject serializedObject, string refProperty, string conditionalRef = null)
        {
            SerializedProperty property = serializedObject.FindProperty(refProperty);

            if (CheckConditional(serializedObject, conditionalRef))
                EditorGUILayout.PropertyField(property);
            else
                WarningRef(conditionalRef);
        }


        public static void DrawLabel(string label, bool defaultStyle = false)
        {
            if (defaultStyle)
                EditorGUILayout.LabelField(label);
            else
                EditorGUILayout.LabelField(label, LabelStyle());
        }

        public static void FoldoutContext(ref bool _foldOut, string _label, Action _context, GUIStyle gS = null)
        {
            if (gS != null)
                _foldOut = EditorGUILayout.Foldout(_foldOut, _label, true, gS);
            else
                _foldOut = EditorGUILayout.Foldout(_foldOut, _label, true);

            if (_foldOut)
            {
                Line();
                EditorGUILayout.Space(10);
                _context();
            }

            Line();
            EditorGUILayout.Space(10);
        }

        public static void Line()
        {
            Rect rect = EditorGUILayout.GetControlRect(false, .5f);
            rect.height = .5f;
            EditorGUI.DrawRect(rect, new Color(.3f, .3f, .3f, 1));
        }

        public static GUIStyle LabelStyle()
        {
            var gS = new GUIStyle();
            gS.normal.textColor = Color.white;
            gS.fontStyle = FontStyle.Bold;
            gS.fontSize = 13;

            return gS;
        }

        public static bool CheckConditional(SerializedObject serializedObject, string conditionalRef)
        {
            if (string.IsNullOrEmpty(conditionalRef))
                return true;

            SerializedProperty conditional = serializedObject.FindProperty(conditionalRef);
            return (conditional.objectReferenceValue != null);
        }

        public static void WarningRef(string varName) =>
            EditorGUILayout.HelpBox($"Referencia necessaria: {varName}", MessageType.Warning);
    }
}
