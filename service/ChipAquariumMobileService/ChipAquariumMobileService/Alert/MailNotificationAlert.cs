using ChipAquariumMobileService.Config;
using ChipAquariumMobileService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ChipAquariumMobileService.Alert
{
    public class MailNotificationAlert : IAlert
    {
        private readonly IAlertDataAccess dataAccess;
        private readonly IAlertMail mail;

        public MailNotificationAlert(IAlertDataAccess dataAccess, IAlertMail mail)
        {
            this.dataAccess = dataAccess;
            this.mail = mail;
        }

        public void Clear()
        {
            dataAccess.Deactivate();
        }

        public async Task RaiseAsync()
        {
            dataAccess.Activate();

            if (dataAccess.HasExceededNotifyAt(DateTimeOffset.Now.Add(AppSettingsReference.AlertRenotifyElapsedTime)))
            {
                await mail.SendAsync();
                dataAccess.Notify();
            }
        }

        public bool ShouldRaise() => false;
    }
}