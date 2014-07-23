using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Daycareinator
{

    public class Notification
    {
        public string NotificationType { get; private set; }
        public string Message { get; private set; }

        public Notification(NotificationType notificationType, string message)
        {
            NotificationType = notificationType.ToString().ToLower();
            Message = message;

        }
        
    }

    public enum NotificationType
    {
        Success,
        Error
    }
}