using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTrip.DAL.Entities;

namespace BookingTrip.BLL.Interfaces
{
    public interface INotificationService
    {
        Task SendNotificationAsync(int userId, string message, NotificationType type);
    }
}
