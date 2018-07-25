using System.Threading.Tasks;

namespace ChipAquariumMobileService.Alert
{
    public interface IAlert
    {
        bool ShouldRaise();

        void Clear();

        Task RaiseAsync();
    }
}