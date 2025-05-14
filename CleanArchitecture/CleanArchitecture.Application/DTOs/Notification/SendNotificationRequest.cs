using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.DTOs.Notification
{
    public class SendNotificationRequest
    {
        public string UserId { get; set; }
        public string Message { get; set; }
    }
}
