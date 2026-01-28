using Microsoft.EntityFrameworkCore;
using ReservasCanchas.DataAccess.Persistance;
using ReservasCanchas.Domain.Entities;
using ReservasCanchas.Domain.Enums;
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

        public async Task<Reservation?> GetReservationByIdWithRelationsAsync(int reservationId)
        {
            return await _context.Reservation
                         .Include(r => r.Field)
                             .ThenInclude(f => f.Complex)
                         .Include(r => r.User)
                         .Include(r => r.Review)
                         .FirstOrDefaultAsync(r => r.Id == reservationId);
        }

        public async Task<List<Reservation>> GetReservationsByUserIdAsync(int userId)
        {
            var reservations = await _context.Reservation
                                        .Include(r => r.Field)
                                            .ThenInclude(f => f.Complex)
                                        .Include(r => r.Review)
                                        .Where(r => r.UserId == userId)
                                        .ToListAsync();
            return reservations; 
        }

        public async Task<List<Reservation>> GetReservationsByComplexIdAsync(int complexId)
        {
            var reservations = await _context.Reservation
                                        .Include(r => r.Field)
                                            .ThenInclude(f => f.Complex)
                                        .Include(r => r.User)
                                        .Include(r => r.Review)
                                        .Where(r => r.Field.Complex.Id == complexId)
                                        .ToListAsync();
            return reservations;
        }


        public async Task<List<Reservation>> GetReservationsByFieldId(int fieldId)
        {
            var reservations = await _context.Reservation
                                        .Include(r => r.Field)
                                            .ThenInclude(f => f.Complex)
                                        .Include(r => r.User)
                                        .Include(r => r.Review)
                                        .Where(r => r.Field.Id == fieldId)
                                        .ToListAsync();
            return reservations;
        }

        public async Task<Reservation?> GetReservationWithReviewByIdAsync(int reservationId)
        {
            var reservation = await _context.Reservation
                            .Include(r => r.Review)
                            .FirstOrDefaultAsync(r => r.Id == reservationId);
            return reservation;
        }

        public async Task<bool> ExistsApprovedReservationAsync(int fieldId, DateOnly date, TimeOnly initTime)
        {
            return await _context.Reservation
                .AnyAsync(r =>
                    r.FieldId == fieldId &&
                    r.Date == date &&
                    r.InitTime == initTime &&
                    (r.ReservationState == ReservationState.Aprobada || r.ReservationState == ReservationState.Pendiente)
                );
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        // METODO AGREGADO PARA EL BACKGROUND
        public async Task<List<Reservation>> GetReservationsToCompleteAsync(DateOnly today, DateTime now)
        {
            var timeNowOnly = TimeOnly.FromDateTime(now);

            return await _context.Reservation
                .Where(r =>
                    r.ReservationState == ReservationState.Aprobada &&
                    (
                        r.Date < today ||
                        (r.Date == today && r.InitTime.AddHours(1) <= timeNowOnly)
                    )
                )
                .ToListAsync();
        }

    }
}
