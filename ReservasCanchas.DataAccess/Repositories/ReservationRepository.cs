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
    public class ReservationRepository
    {
        private readonly AppDbContext _context;
        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Reservation> CreateReservationAsync(Reservation reservation)
        {
            _context.Reservation.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task<List<Reservation>> GetReservationsByUserAsync(int userId)
        {
            var reservations = await _context.Reservation
                                        .Include(r => r.Field)
                                            .ThenInclude(f => f.Complex)
                                        .Include(r => r.Review)
                                        .Where(r => r.UserId == userId)
                                        .ToListAsync();
            return reservations; 
        }
    }
}
