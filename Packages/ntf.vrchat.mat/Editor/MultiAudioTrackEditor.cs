using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;
using System;


#if !COMPILER_UDONSHARP && UNITY_EDITOR && false
using UnityEditor;
using UdonSharpEditor;

namespace NTF.VRChat
{
    [CustomEditor(typeof(MultiAudioTrack))]
    public class MultiAudioTrackEditor : Editor
    {
        private SerializedProperty clips_property;

        private AudioClip[] clips;
        private AudioSource audioSource;
        private void OnEnable()
        {
            clips_property = serializedObject.FindProperty("Clips");
        }
        public override void OnInspectorGUI()
        {
            if (UdonSharpGUI.DrawDefaultUdonSharpBehaviourHeader(target)) return;

            MultiAudioTrack multiAudioTrack = (MultiAudioTrack)target;

            EditorGUI.BeginChangeCheck();

            // get fields here

            serializedObject.Update();
            EditorGUILayout.PropertyField(clips_property, true);
            serializedObject.ApplyModifiedProperties();

            if (EditorGUI.EndChangeCheck())
            {
                // update fields here
                Undo.RecordObject(multiAudioTrack, "Modifed MultiAudioTrack");
            }
        }
    }
}

#endif