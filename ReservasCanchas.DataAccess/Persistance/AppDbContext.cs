using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReservasCanchas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.DataAccess.Persistance
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options) { }

        public DbSet<Complex> Complex {  get; set; }
        public DbSet<Service> Service { get; set; }
        //public DbSet<User> Users { get; set; }                 identity ya me maneja los usuarios
        public DbSet<Field> Field { get; set; }
        public DbSet<RecurringFieldBlock> RecurringFieldBlock { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<TimeSlotComplex> TimeSlotComplex { get; set; }
        public DbSet<TimeSlotField> TimeSlotField { get; set; }
        public DbSet<Notification> Notification { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .ToTable("Users");

            //Modelo la relacion muchos a muchos, dejo que EF Core me cree la tabla intermedia 'ComplexService' automaticamente
            modelBuilder.Entity<Complex>()
                .HasMany(c => c.Services)
                .WithMany(s => s.Complexes)
                .UsingEntity(j => j.ToTable("ComplexService"));

            // Modelo la relacion 1 a 1 de complejo con la franja horaria del complejo
            modelBuilder.Entity<Complex>()
                .HasMany(c => c.TimeSlots)
                .WithOne(t => t.Complex)
                .HasForeignKey(j => j.ComplexId);

            // Modelo la relacion 1 a n de complejo con la franja horaria
            /*modelBuilder.Entity<TimeSlotComplex>()
                .HasOne(c => c.Complex)
                .WithMany(t => t.TimeSlots)
                .HasForeignKey(j => j.ComplexId);*/



            // Modelo la relacion 1 a muchos de complejo y canchas
            modelBuilder.Entity<Complex>()
                .HasMany(c => c.Fields)
                .WithOne(c => c.Complex)
                .HasForeignKey(f => f.ComplexId);

            // Modelo relacion 1 a muchos de complejo y usuario
            modelBuilder.Entity<Complex>()
                .HasOne(u => u.User)
                .WithMany(c => c.Complejos)
                .HasForeignKey(c => c.UserId);

            // Modelo la relacion 1 a muchos de usuario y reservas
            modelBuilder.Entity<Reservation>()
                .HasOne(u => u.User)
                .WithMany(r => r.Reservations)
                .HasForeignKey(u => u.UserId);

            // Modelo la relacion 1 a muchos de usuario y reserñas
            modelBuilder.Entity<Review>()
                .HasOne(u => u.User)
                .WithMany(r => r.Reviews)
                .HasForeignKey(u => u.UserId);

            // Modelo la relacion 1 a 0..1 de reseña y reserva
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Reservation)
                .WithOne(a => a.Review)
                .HasForeignKey<Review>(j => j.ReservationId)
                .IsRequired(false);

            // Modelo relacion 1 a 1 entre cancha y franja horaria de la cancha
            modelBuilder.Entity<Field>()
                .HasMany(c => c.TimeSlotsField)
                .WithOne(t => t.Field)
                .HasForeignKey(j => j.FieldId);

            // Modelo relacion 1 a muchos entre cancha y los bloqueos recurrentes
            modelBuilder.Entity<Field>()
                .HasMany(c => c.RecurringCourtBlocks)
                .WithOne(t => t.Field)
                .HasForeignKey(f => f.FieldId);

            // Modelo relacion 1 a muchos entre cancha y reservas
            modelBuilder.Entity<Field>()
                .HasMany(c => c.Reservations)
                .WithOne(t => t.Field)
                .HasForeignKey(f => f.FieldId);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // PASO 7 USO DE JWT (paso 8 vuelvo a account controller)
            //agrego los roles que va a tener mi aplicacion
            List<IdentityRole<int>> roles = new List<IdentityRole<int>>
            {
                new IdentityRole<int>
                {
                    Id = 1,
                    Name = "Usuario",
                    NormalizedName = "USUARIO"
                },
                new IdentityRole < int >
                {
                    Id = 2,
                    Name = "AdminComplejo",
                    NormalizedName = "ADMINCOMPLEJO"
                },
                new IdentityRole < int >
                {
                    Id = 3,
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN"
                },
            };
            modelBuilder.Entity<IdentityRole<int>>().HasData(roles);

        }
    }
}
