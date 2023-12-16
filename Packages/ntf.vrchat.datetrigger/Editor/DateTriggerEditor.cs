using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;
using System;
using UnityEngine.UIElements;


#if !COMPILER_UDONSHARP && UNITY_EDITOR
using UnityEditor;
using UdonSharpEditor;

namespace NTF.VRChat
{
    [CustomEditor(typeof(DateTrigger))]
    public class DateTriggerEditor : Editor
    {
        private DateProvider DateProvider;
        private DateTriggerRangeMethod RangeMethod = DateTriggerRangeMethod.Inclusive;
        private string ParameterName = "DateTrigger";
        private Animator Animator = null;
        // Date
        private bool EnabledDay = false;
        private int FromDay = 0;
        private int ToDay = 0;

        private bool EnabledWeek = false;
        private DayOfWeek FromWeek = 0;
        private DayOfWeek ToWeek = 0;

        private bool EnabledMonth = false;
        private int FromMonth = 0;
        private int ToMonth = 0;

        private bool EnabledYear = false;
        private int FromYear = 0;
        private int ToYear = 0;
        // Time
        private bool EnabledHour = false;
        private int FromHour = 0;
        private int ToHour = 0;

        private bool EnabledMinute = false;
        private int FromMinute = 0;
        private int ToMinute = 0;

        private bool EnabledSecond = false;
        private int FromSecond = 0;
        private int ToSecond = 0;
        public override void OnInspectorGUI()
        {
            if (UdonSharpGUI.DrawDefaultUdonSharpBehaviourHeader(target)) return;

            DateTrigger dateTrigger = (DateTrigger)target;

            EditorGUI.BeginChangeCheck();

            // Put fields here
            DateProvider = (DateProvider)EditorGUILayout.ObjectField(new GUIContent("DateProvider","The object for providing the DateTime struct"), dateTrigger.DateProvider, typeof(DateProvider), true);
            if (DateProvider == null)
                EditorGUILayout.HelpBox("DateProvider is required!", MessageType.Error);
            RangeMethod = (DateTriggerRangeMethod)EditorGUILayout.EnumPopup(new GUIContent("Range Method","To check inclusive/exclusive"), dateTrigger.RangeMethod);
            Animator = (Animator)EditorGUILayout.ObjectField(new GUIContent("Animator", "The Animator Controller to use"), dateTrigger.Animator, typeof(Animator), true);
            if (Animator == null)
                EditorGUILayout.HelpBox("Animator is required", MessageType.Error);
            ParameterName = EditorGUILayout.TextField(new GUIContent("Animator Parameter", "The name of the Animator Parameter in the Controller. Should be a bool."), dateTrigger.ParameterName);
            if (ParameterName == null || ParameterName.Length == 0)
                EditorGUILayout.HelpBox("Animator Parameter cannot be empty!", MessageType.Error);

            var day = RangeField(dateTrigger.EnabledDay, dateTrigger.FromDay, dateTrigger.ToDay, new GUIContent("Day"));
            EnabledDay = day.Item1;
            FromDay = day.Item2;
            ToDay = day.Item3;
            var week = RangeField(dateTrigger.EnabledWeek, dateTrigger.FromWeek, dateTrigger.ToWeek, new GUIContent("Week"));
            EnabledWeek = week.Item1;
            FromWeek = week.Item2;
            ToWeek = week.Item3;
            var month = RangeField(dateTrigger.EnabledMonth, dateTrigger.FromMonth, dateTrigger.ToMonth, new GUIContent("Month"));
            EnabledMonth = month.Item1;
            FromMonth = month.Item2;
            ToMonth = month.Item3;
            var year = RangeField(dateTrigger.EnabledYear, dateTrigger.FromYear, dateTrigger.ToYear, new GUIContent("Year"));
            EnabledYear = year.Item1;
            FromYear = year.Item2;
            ToYear = year.Item3;
            var hour = RangeField(dateTrigger.EnabledHour, dateTrigger.FromHour, dateTrigger.ToHour, new GUIContent("Hour"));
            EnabledHour = hour.Item1;
            FromHour = hour.Item2;
            ToHour = hour.Item3;
            var minute = RangeField(dateTrigger.EnabledMinute, dateTrigger.FromMinute, dateTrigger.ToMinute, new GUIContent("Minute"));
            EnabledMinute = minute.Item1;
            FromMinute = minute.Item2;
            ToMinute = minute.Item3;
            var second = RangeField(dateTrigger.EnabledSecond, dateTrigger.FromSecond, dateTrigger.ToSecond, new GUIContent("Second"));
            EnabledSecond = second.Item1;
            FromSecond = second.Item2;
            ToSecond = second.Item3;

            if(EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(dateTrigger, "Modifed DateTrigger");
                // set fields to object here
                dateTrigger.DateProvider = DateProvider;
                dateTrigger.RangeMethod = RangeMethod;
                dateTrigger.Animator = Animator;
                dateTrigger.ParameterName = ParameterName;

                dateTrigger.EnabledDay = EnabledDay;
                dateTrigger.EnabledWeek = EnabledWeek;
                dateTrigger.EnabledMonth = EnabledMonth;
                dateTrigger.EnabledYear = EnabledYear;
                dateTrigger.EnabledHour = EnabledHour;
                dateTrigger.EnabledMinute = EnabledMinute;
                dateTrigger.EnabledSecond = EnabledSecond;

                dateTrigger.FromDay = FromDay;
                dateTrigger.ToDay = ToDay;
                dateTrigger.FromWeek = FromWeek;
                dateTrigger.ToWeek = ToWeek;
                dateTrigger.FromMonth = FromMonth;
                dateTrigger.ToMonth = ToMonth;
                dateTrigger.FromYear = FromYear;
                dateTrigger.ToYear = ToYear;

                dateTrigger.FromHour = FromHour;
                dateTrigger.ToHour = ToHour;
                dateTrigger.FromMinute = FromMinute;
                dateTrigger.ToMinute = ToMinute;
                dateTrigger.FromSecond = FromSecond;
                dateTrigger.ToSecond = ToSecond;
            }
        }
        private (bool,int,int) RangeField(bool enabled,int from,int to,GUIContent content)
        {
            bool _enabled = EditorGUILayout.BeginToggleGroup(content, enabled);
            int _from = EditorGUILayout.IntField(new GUIContent("From"), from);
            int _to = EditorGUILayout.IntField(new GUIContent("To"), to);
            if (_from > _to)
                EditorGUILayout.HelpBox("From is bigger than to", MessageType.Warning);
            if (_from == _to)
                EditorGUILayout.HelpBox("This will EXACTLY check for this value", MessageType.Info);
            EditorGUILayout.EndToggleGroup();
            return (_enabled, _from, _to);
        }
        private (bool, DayOfWeek, DayOfWeek) RangeField(bool enabled, DayOfWeek from, DayOfWeek to, GUIContent content)
        {
            bool _enabled = EditorGUILayout.BeginToggleGroup(content, enabled);
            DayOfWeek _from = (DayOfWeek)EditorGUILayout.EnumPopup(new GUIContent("From"), from);
            DayOfWeek _to = (DayOfWeek)EditorGUILayout.EnumPopup(new GUIContent("To"), to);
            if (_enabled && _from > _to)
                EditorGUILayout.HelpBox("From is bigger than to", MessageType.Warning);
            if (_enabled && _from == _to)
                EditorGUILayout.HelpBox("This will EXACTLY check for this value", MessageType.Info);
            EditorGUILayout.EndToggleGroup();
            return (_enabled, _from, _to);
        }
    }
}
#endif