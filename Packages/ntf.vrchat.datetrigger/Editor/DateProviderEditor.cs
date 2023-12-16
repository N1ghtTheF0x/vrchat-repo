using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;
using System;

#if !COMPILER_UDONSHARP && UNITY_EDITOR
using UnityEditor;
using UdonSharpEditor;

namespace NTF.VRChat
{
    [CustomEditor(typeof(DateProvider))]
    public class DateProviderEditor : Editor
    {
        private DateTriggerDateTimeMethod DateTimeMethod;
        public override void OnInspectorGUI()
        {
            if (UdonSharpGUI.DrawDefaultUdonSharpBehaviourHeader(target)) return;
            DateProvider dateProvider = (DateProvider)target;

            EditorGUI.BeginChangeCheck();

            DateTimeMethod = (DateTriggerDateTimeMethod)EditorGUILayout.EnumPopup(new GUIContent("Retrieval Method","The method of getting the current date/time"), dateProvider.DateTimeMethod);

            if(EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(dateProvider, "Modified DateProvider");
                dateProvider.DateTimeMethod = DateTimeMethod;
            }
        }
    }
}

#endif