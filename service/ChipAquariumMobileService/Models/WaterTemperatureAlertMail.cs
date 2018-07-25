using System.Net.Mail;
using SendGrid;
using ChipAquariumMobileService.Config;
using System.Globalization;
using System.Threading.Tasks;
using ChipAquariumMobileService.Properties;
using System.Collections.Generic;

namespace ChipAquariumMobileService.Models
{
    public class WaterTemperatureAlertMail
    {
        private readonly SendGridMessage message;

        public WaterTemperatureAlertMail(int aquariumId, float temperature)
        {
            message = CreateMessage(aquariumId, temperature);
        }

        private static SendGridMessage CreateMessage(int aquariumId, float temperature)
        {
            var message = new SendGridMessage();

            message.From = new MailAddress(AppNameValueSetting.AlertMailFromAddress, AppNameValueSetting.AlertMailFromDisplayName);

            var tos = new[] { AppNameValueSetting.AlertMailToAddress };
            message.AddTo(tos);
            message.Header.SetTo(tos);

            message.Subject = Resources.WaterTemperatureAlertMailSubject;

            message.Text = Resources.WaterTemperatureAlertMailText;
            message.AddSubstitution(Resources.WaterTemperatureAlertMailTextAquariumIdSection, new List<string>() { aquariumId.ToString(CultureInfo.InvariantCulture) } );
            message.AddSubstitution(Resources.WaterTemperatureAlertMailTextTemperatureSection, new List<string>() { temperature.ToString(CultureInfo.InvariantCulture) } );

            return message;
        }

        public Task SendAsync(MailTransport transport)
        {
            return transport.SendAsync(message);
        }
    }
}