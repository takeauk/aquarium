using System.Net.Mail;
using SendGrid;
using ChipAquariumMobileService.Config;
using System.Globalization;
using System.Threading.Tasks;
using ChipAquariumMobileService.Properties;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ChipAquariumMobileService.Alert
{
    public class AlertMail : IAlertMail
    {
        private readonly Web web;

        private readonly SendGridMessage message;

        public AlertMail(string text)
        {
            web = new Web(AppSettingsReference.SendGridApiKey);

            message = new SendGridMessage();
            message.From = new MailAddress(AppSettingsReference.AlertMailFromAddress, AppSettingsReference.AlertMailFromDisplayName);

            var addresses = new[] { AppSettingsReference.AlertMailToAddress };
            message.AddTo(addresses);
            message.Header.SetTo(addresses);

            message.Subject = Resources.WaterTemperatureAlertMailSubject;
            message.Text = text;
        }

        public void AddSubstitution(string replacementTag, params object[] substitutionValues) => 
            message.AddSubstitution(replacementTag, substitutionValues.Select(o => Convert.ToString(o, CultureInfo.InvariantCulture)).ToList());

        public async Task SendAsync() => await web.DeliverAsync(message);
    }
}