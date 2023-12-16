
using NTF.VRChat;
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace NTF.VRChat
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Continuous)]
    [AddComponentMenu("N1ghtTheF0x/VRChat/DateProvider")]
    public class DateProvider : UdonSharpBehaviour
    {
        public DateTriggerDateTimeMethod DateTimeMethod;
        public DateTime CurrentDateTime;
        private void Start()
        {
            CurrentDateTime = GetDateTime();
        }
        private void Update()
        {
            CurrentDateTime = GetDateTime();
        }
        private DateTime GetDateTime()
        {
            return DateTriggerUtils.GetDateTime(DateTimeMethod);
        }
    }
}
