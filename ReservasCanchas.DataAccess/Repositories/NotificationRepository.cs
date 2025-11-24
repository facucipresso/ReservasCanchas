using Microsoft.EntityFrameworkCore;
using ReservasCanchas.DataAccess.Persistance;
using ReservasCanchas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.DataAccess.Repositories
{
    public class NotificationRepository
    {
        private AppDbContext _context;

        public NotificationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Notification?> GetNotificationByIdAsync(int id)
        {
            return await _context.Notification.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<Notification> CreateNotificationAsync(Notification notification)
        {
            _context.Notification.Add(notification);
            await _context.SaveChangesAsync();
            return notification;
        }

        public async Task SaveAsync()
        {
            _context.SaveChangesAsync();
        }

        public async Task<List<Notification>> GetNotificationsByUserIdAsync(int userId)
        {
            var notifications = await _context.Notification
                .Where(n => n.UserId == userId)
                .ToListAsync();
            return notifications;
        }
    }
}
