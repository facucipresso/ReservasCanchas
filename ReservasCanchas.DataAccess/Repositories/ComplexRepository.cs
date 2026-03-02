using Microsoft.EntityFrameworkCore;
using ReservasCanchas.DataAccess.Persistance;
using ReservasCanchas.Domain.Enums;
using Complex = ReservasCanchas.Domain.Entities.Complex;

namespace ReservasCanchas.DataAccess.Repositories
{
    public class ComplexRepository
    {
        private readonly AppDbContext _context;
        public ComplexRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Complex?> GetComplexByIdWithBasicInfoAsync(int id)
        {
            var complex = await _context.Complex
                                .Include(c => c.TimeSlots)
                                .Include(c => c.Services)
                                .FirstOrDefaultAsync(c => c.Id == id && c.Active);
            return complex;
        }

        public async Task<Complex?> GetComplexByIdWithFieldsAsync(int id)
        {
            var complexWithRelations = await _context.Complex
                                        .Include(c => c.Fields.Where(f => f.Active))
                                            .ThenInclude(f => f.RecurringCourtBlocks)
                                        .Include(c => c.Fields.Where(f => f.Active))
                                            .ThenInclude(f => f.TimeSlotsField)
                                        .Include(c => c.Services)
                                        .Include(c => c.TimeSlots)
                                        .FirstOrDefaultAsync(c => c.Id == id && c.Active);
            return complexWithRelations;
        }

        public async Task<Complex?> GetComplexByIdWithReservationsAsync(int id)
        {
            return await _context.Complex
                        .Include(c => c.Fields)
                            .ThenInclude(f => f.Reservations)
                                .ThenInclude(r => r.User)

                        .Include(c => c.Fields)
                            .ThenInclude(f => f.RecurringCourtBlocks)

                        .Include(c => c.TimeSlots)

                        .FirstOrDefaultAsync(c => c.Id == id && c.Active && c.ComplexState == ComplexState.Habilitado);
        }

        public async Task<List<Complex>> GetAllComplexesAsync()
        {
            //Solo lo llama el SUPERADMIN
            var complexes = await _context.Complex
                                .Where(c => c.Active)
                                .ToListAsync();
            return complexes;
        }

        public async Task<List<Complex>> GetComplexesByUserIdAsync(int userId)
        {
            return await _context.Complex
                         .Include(c => c.Fields)
                         .Where(c => c.UserId == userId && c.Active)
                         .ToListAsync();
        }

        public async Task<Complex> CreateComplexAsync(Complex complex)
        {
            _context.Complex.Add(complex);
            await _context.SaveChangesAsync();
            return complex;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByNameAsync(string complexName)
        {
            return await _context.Complex.AnyAsync(c => c.Name.ToLower() == complexName.ToLower() && c.Active);
        }

        public async Task<bool> ExistsByAddressAsync(string street, string number, string locality, string province)
        {
            return await _context.Complex.AnyAsync(c => c.Street.ToLower() == street.ToLower() &&
                                                        c.Number.ToLower() == number.ToLower() &&
                                                        c.Locality.ToLower() == locality.ToLower() &&
                                                        c.Province.ToLower() == province.ToLower() &&
                                                        c.Active);
        }

        public async Task<bool> ExistsByPhoneAsync(string phone)
        {
            return await _context.Complex.AnyAsync(c => c.Phone.ToLower() == phone.ToLower() && c.Active);
        }

        public async Task<List<Complex>> GetComplexesWithFiltersAsync(string province,string locality, FieldType fieldType)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var limit = today.AddDays(7);

            var complexes = await _context.Complex
                .Include(c => c.TimeSlots)
                .Include(c => c.Fields)                      
                    .ThenInclude(f => f.Reservations
                        .Where(r => r.Date >= today && r.Date <= limit))
                .Include(c => c.Fields)                      
                    .ThenInclude(f => f.RecurringCourtBlocks)
                .Where(c =>
                    c.Province == province &&
                    (string.IsNullOrWhiteSpace(locality) || c.Locality == locality) &&
                    c.Active &&
                    c.ComplexState == ComplexState.Habilitado
                )
                .ToListAsync();

            foreach (var c in complexes)
            {
                c.Fields = c.Fields
                    .Where(f =>
                        f.Active &&
                        f.FieldType == fieldType &&
                        f.FieldState == FieldState.Habilitada)
                    .ToList();
            }

            return complexes;
        }

        public async Task<double?> GetAverageRatingAsync(int complexId)
        {
            return await _context.Complex
                .Where(c => c.Id == complexId)
                .SelectMany(c => c.Fields)                 
                .SelectMany(f => f.Reservations)           
                .Where(r => r.Review != null)              
                .Select(r => (double?)r.Review.Score)
                .AverageAsync();
        }

        public async Task<bool> HasActiveReservationsInComplexAsync (int id)
        {
            return await _context.Complex
                        .Where(c => c.Id == id && c.Active)
                            .AnyAsync(c => c.Fields
                                .Any(f => f.Reservations
                                    .Any(r => r.ReservationState == ReservationState.Aprobada)));
        }

        public async Task<int> GetNumberOfComplexEnabled()
        {
            var numberOfComplexEnabled = await _context.Complex
                .Where(c => c.ComplexState == ComplexState.Habilitado)
                .CountAsync();

            return numberOfComplexEnabled;
        }

        public async Task<List<Complex>> GetLastFiveComplexAsync()
        {
            var lastFourComplexes = _context.Complex
                .Where(c => c.ComplexState == ComplexState.Habilitado)
                .OrderByDescending(e => e.Id)
                .Take(5)
                .ToList();

            return lastFourComplexes; 
        }
    }
}
