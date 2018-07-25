using ChipAquariumMobileService.Properties;
using System.Globalization;
using System.Threading.Tasks;

namespace ChipAquariumMobileService.Alert
{
    public class WaterTemperatureOutOfRangeAlertMail : IAlertMail
    {
        private readonly AlertMail mail;

        public WaterTemperatureOutOfRangeAlertMail(int aquariumId, float temperature)
        {
            this.mail = new AlertMail(Resources.WaterTemperatureOutOfRangeAlertMailText);
            this.mail.AddSubstitution(Resources.AlertMailTextAquariumIdSection, aquariumId);
            this.mail.AddSubstitution(Resources.WaterTemperatureOutOfRangeAlertMailTextTemperatureSection, temperature);
        }

        public async Task SendAsync() => await mail.SendAsync();
    }
}