using Microsoft.EntityFrameworkCore;
using ReservasCanchas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.DataAccess.Persistance
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options) { }

        public DbSet<Complejo> Complejo {  get; set; }
        public DbSet<ComplexService> ComplexService { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Modelo relacion 1 a N entre Complejo y ComplexService
            modelBuilder.Entity<ComplexService>()
                .HasOne(cs => cs.Complejo)
                .WithMany(c => c.Services)
                .HasForeignKey(cs => cs.ComplexId);

            //Guardo el enum como string en la BBDD
            modelBuilder.Entity<ComplexService>()
                .Property(cs => cs.Service)
                .HasConversion<string>();

            //Para que la PK sea compuesta y no tenga id autoincremental
            modelBuilder.Entity<ComplexService>()
                .HasKey(cs => new { cs.ComplexId, cs.Service });
        }
    }
}
