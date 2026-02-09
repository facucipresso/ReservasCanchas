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
    public class ReviewRepository
    {
        private readonly AppDbContext _context;
        public ReviewRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Review?> GetReviewByIdAsync(int id)
        {
           return await _context.Review
                .Where(r => r.Id == id)
                .Include(r => r.User)
                .Include(r => r.Reservation)
                .FirstOrDefaultAsync();
        }

        public async Task<Review> CreateReviewAsync(Review review)
        {
            _context.Review.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<Review?> GetReviewByReservationIdAsync(int reservationId)
        {
            return await _context.Review
                .Where(r =>  reservationId == r.ReservationId)
                .Include(r => r.User)
                .Include(r => r.Reservation)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Review>> GetReviewsByComplexIdAsync(int complexId)
        {
            return await _context.Review
                .Include(r => r.User)
                .Include(r => r.Reservation)
                    .ThenInclude(res => res.Field)
                .Where(r => r.Reservation.Field.ComplexId == complexId)
                .OrderByDescending(r => r.CreationDate)
                .Take(10)
                .ToListAsync();
        }

        public async Task<List<Review>> GetReviewsByUserIdAsync(int userId)
        {
            return await _context.Review
                .Include(r => r.User)
                .Include(r => r.Reservation)
                .Where(r =>  userId == r.UserId)
                .ToListAsync();
        }

        public async Task DeleteReviewAsync(Review review)
        {
            _context.Review.Remove(review);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetNumberOfReviews()
        {
            var numberOfReviews = await _context.Review.CountAsync();
            return numberOfReviews;
        }

        public async Task<List<Review>> GetLastFourReviewAsync()
        {
            var lastFourReviews = _context.Review
                .OrderByDescending(e => e.CreationDate)
                .Take(4)
                .ToList();

            return lastFourReviews;
        }

    }
}
