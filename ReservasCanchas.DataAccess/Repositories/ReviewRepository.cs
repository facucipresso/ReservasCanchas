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

    }
}
