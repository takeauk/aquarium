using System;

namespace ChipAquariumMobileService.Models
{
    public interface IAlertDataAccess
    {
        void Activate();
        void Deactivate();
        bool HasExceededNotifyAt(DateTimeOffset value);
        void Notify();
    }
}