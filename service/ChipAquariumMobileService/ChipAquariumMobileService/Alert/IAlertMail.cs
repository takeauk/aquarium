using System.Threading.Tasks;

namespace ChipAquariumMobileService.Alert
{
    public interface IAlertMail
    {
        Task SendAsync();
    }
}