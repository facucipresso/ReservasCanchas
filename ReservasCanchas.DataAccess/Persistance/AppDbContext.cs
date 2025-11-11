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
        public DbSet<Service> Service { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Modelo la relacion muchos a muchos, dejo que EF Core me cree la tabla intermedia 'ComplexService' automaticamente
            modelBuilder.Entity<Complejo>()
                .HasMany(c => c.Services)
                .WithMany(s => s.Complexes)
                .UsingEntity(j => j.ToTable("ComplexService"));

            // Modelo la relacion 1 a 1 de complejo con la franja horaria del complejo
            modelBuilder.Entity<Complejo>()
                .HasOne(c => c.TimeSlotComplex)
                .WithOne(t => t.Complex)
                .HasForeignKey<TimeSlotComplex>(j => j.ComplexId);


            // Modelo la relacion 1 a muchos de complejo y canchas
            modelBuilder.Entity<Complejo>()
                .HasMany(c => c.Fields)
                .WithOne(c => c.Complex)
                .HasForeignKey(f => f.ComplexId);

            // Modelo relacion 1 a muchos de complejo y usuario
            modelBuilder.Entity<Complejo>()
                .HasOne(u => u.Usuario)
                .WithMany(c => c.Complejos)
                .HasForeignKey(c => c.UserId);

            // Modelo la relacion 1 a muchos de usuario y reservas
            modelBuilder.Entity<Reservation>()
                .HasOne(u => u.Usuario)
                .WithMany(r => r.Reservations)
                .HasForeignKey(u => u.UserId);

            // Modelo la relacion 1 a muchos de usuario y reserñas
            modelBuilder.Entity<Review>()
                .HasOne(u => u.Usuario)
                .WithMany(r => r.Reviews)
                .HasForeignKey(u => u.IdUsuario);

            // Modelo la relacion 1 a 0..1 de reseña y reserva
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Reservation)
                .WithOne(a => a.Review)
                .HasForeignKey<Review>(j => j.ReservationId)
                .IsRequired(false);

            // Modelo relacion 1 a 1 entre cancha y franja horaria de la cancha
            modelBuilder.Entity<Field>()
                .HasOne(c => c.TimeSlotField)
                .WithOne(t => t.Field)
                .HasForeignKey<TimeSlotField>(j => j.FieldId);

            // Modelo relacion 1 a muchos entre cancha y los bloqueos recurrentes
            modelBuilder.Entity<Field>()
                .HasMany(c => c.recurringCourtBlocks)
                .WithOne(t => t.Field)
                .HasForeignKey(f => f.FieldId);

            // Modelo relacion 1 a muchos entre cancha y reservas
            modelBuilder.Entity<Field>()
                .HasMany(c => c.Reservations)
                .WithOne(t => t.Field)
                .HasForeignKey(f => f.FieldId);
        }
    }
}
