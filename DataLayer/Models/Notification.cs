namespace ColicheGassose
{
    using System;
    using System.Collections.Generic;
    
    public partial class Notification
    {

        /// <summary>
        /// Generate a Notification to be pushed to the App (only if scheduled at least 5 minutes in the future)
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Notification Generate(DateTime when, string token, string message)
        {
            if ((when - DateTime.Now).TotalMinutes > 5)
            {
                var result = new Notification();

                result.DeviceToken = token;
                result.When = when;
                result.Message = message;

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
