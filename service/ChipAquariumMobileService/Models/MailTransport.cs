using ChipAquariumMobileService.Config;
using SendGrid;
using System.Threading.Tasks;

namespace ChipAquariumMobileService.Models
{
    public class MailTransport
    {
        private readonly Web web;

        public MailTransport()
        {
            web = new Web(AppNameValueSetting.SendGridApiKey);
        }

        public async Task SendAsync(ISendGrid message)
        {
            await web.DeliverAsync(message);
        }
    }
}