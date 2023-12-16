
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace NTF.VRChat
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Continuous)]
    [AddComponentMenu("N1ghtTheF0x/VRChat/DateTrigger")]
    public class DateTrigger : UdonSharpBehaviour
    {
        private bool _isActive;
        private bool _lastActive;

        public DateProvider DateProvider;
        public DateTriggerRangeMethod RangeMethod;
        public string ParameterName;
        public Animator Animator;
        // Date
        public bool EnabledDay;
        public int FromDay;
        public int ToDay;

        public bool EnabledWeek;
        public DayOfWeek FromWeek;
        public DayOfWeek ToWeek;

        public bool EnabledMonth;
        public int FromMonth;
        public int ToMonth;

        public bool EnabledYear;
        public int FromYear;
        public int ToYear;
        // Time
        public bool EnabledHour;
        public int FromHour;
        public int ToHour;

        public bool EnabledMinute;
        public int FromMinute;
        public int ToMinute;

        public bool EnabledSecond;
        public int FromSecond;
        public int ToSecond;
        private void Start()
        {
            _isActive = false;
            _lastActive = false;
            Balls();
        }
        private void Update()
        {
            Balls();
        }
        private void Balls()
        {
            if (DateProvider == null)
                return;
            DateTime dateTime = DateProvider.CurrentDateTime;
            _isActive = CheckDateTime(dateTime);
            if (_isActive != _lastActive)
            {
                _lastActive = _isActive;
                PlayAnimator();
            }
        }
        private bool CheckDateTime(DateTime dateTime)
        {
            bool active = false;
            if(EnabledDay)
                active = RangeCheck(dateTime.Day,FromDay,ToDay);
            if(EnabledWeek)
                active = RangeCheck(dateTime.DayOfWeek,FromWeek,ToWeek);
            if(EnabledMonth)
                active = RangeCheck(dateTime.Month,FromMonth,ToMonth);
            if(EnabledYear)
                active = RangeCheck(dateTime.Year,FromYear,ToYear);
            if(EnabledHour)
                active = RangeCheck(dateTime.Hour,FromHour,ToHour);
            if(EnabledMinute)
                active = RangeCheck(dateTime.Minute,FromMinute,ToMinute);
            if(EnabledSecond)
                active = RangeCheck(dateTime.Second,FromSecond,ToSecond);
            return active;
        }
        private bool RangeCheck(int value, int from, int to)
        {
            switch(RangeMethod)
            {
                default:
                case DateTriggerRangeMethod.Inclusive: return DateTriggerUtils.InInclusiveRange(value, from, to);
                case DateTriggerRangeMethod.Exclusive: return DateTriggerUtils.InExclusiveRange(value, from, to);
            }
        }
        private bool RangeCheck(DayOfWeek value, DayOfWeek from, DayOfWeek to)
        {
            switch (RangeMethod)
            {
                default:
                case DateTriggerRangeMethod.Inclusive: return DateTriggerUtils.InInclusiveRange(value, from, to);
                case DateTriggerRangeMethod.Exclusive: return DateTriggerUtils.InExclusiveRange(value, from, to);
            }
        }
        private void PlayAnimator()
        {
            if(Animator != null)
                Animator.SetBool(ParameterName, _isActive);
        }
    }
}
