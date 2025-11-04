using Demo2.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo2.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
        public DbSet<Complex> Complexes { get; set; }
    }
}
