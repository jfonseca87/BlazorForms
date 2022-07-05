using BlazorForms.Models;
using System;

namespace BlazorForms.Services
{
    public class ComunicationService
    {
        public event Action<Notification> NotificationSubscriptor;

        public void SendNotification(Notification notification)
        {
            NotificationSubscriptor?.Invoke(notification);
        }
    }
}
