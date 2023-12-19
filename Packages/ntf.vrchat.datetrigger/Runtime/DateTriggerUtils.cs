using System;
using UnityEngine;
using VRC.SDKBase;

namespace NTF.VRChat
{
    public static class DateTriggerUtils
    {
        public static bool InInclusiveRange(int value,int from,int to)
        {
            return value >= from && value <= to;
        }
        public static bool InInclusiveRange(DayOfWeek value,DayOfWeek from,DayOfWeek to)
        {
            return InInclusiveRange((int)value,(int)from,(int)to);
        }
        public static bool InExclusiveRange(int value, int from, int to)
        {
            return value > from && value < to;
        }
        public static bool InExclusiveRange(DayOfWeek value, DayOfWeek from, DayOfWeek to)
        {
            return InExclusiveRange((int)value,(int)from,(int)to);
        }
        public static DateTime GetDateTime(DateTriggerDateTimeMethod method)
        {
            switch (method)
            {
                default:
                case DateTriggerDateTimeMethod.System: return DateTime.Now;
                case DateTriggerDateTimeMethod.Networking: return Networking.GetNetworkDateTime();
            }
        }
    }
}
