using ChipAquariumMobileService.Properties;
using System;
using System.Threading.Tasks;

namespace ChipAquariumMobileService.Alert
{
    internal class NotBeenReceivedAlertMail : IAlertMail
    {
        private readonly AlertMail mail;

        public NotBeenReceivedAlertMail(DateTimeOffset lastReceivedAt) 
        {
            this.mail = new AlertMail(Resources.NotBeenReceivedAlertMailText);
            this.mail.AddSubstitution(Resources.NotBeenReceivedAlertMailText, lastReceivedAt);
        }

        public async Task SendAsync() => await mail.SendAsync();
    }
}