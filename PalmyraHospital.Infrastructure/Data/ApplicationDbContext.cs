using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PalmyraHospital.Domain.Entities;
using PalmyraHospital.Infrastructure.Identity;

namespace PalmyraHospital.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Specialization> Specializations => Set<Specialization>();


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ConfigureDoctor(builder);
            ConfigurePatient(builder);
            ConfigureAppointment(builder);
            ConfigureDepartment(builder);
            ConfigureSpecialization(builder);
        }


        private void ConfigureDoctor(ModelBuilder builder)
        {
            builder.Entity<Doctor>(entity =>
            {
                entity.ToTable("Doctors");

                entity.HasKey(d => d.Id);

                entity.Property(d => d.DoctorNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(d => d.LicenseNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(d => d.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(d => d.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(d => d.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(d => d.ConsultationFee)
                    .HasPrecision(18, 2);

                entity.Property(d => d.CreatedAt)
                    .IsRequired();

                entity.Property(d => d.IsArchived)
                    .HasDefaultValue(false);


                entity.HasOne(d => d.Department)
                    .WithMany(dep => dep.Doctors)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);


                entity.HasOne(d => d.Specialization)
                    .WithMany(s => s.Doctors)
                    .HasForeignKey(d => d.SpecializationId)
                    .OnDelete(DeleteBehavior.Restrict);


                entity.HasOne<ApplicationUser>()
                    .WithOne(u => u.Doctor)
                    .HasForeignKey<Doctor>(d => d.UserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);


                entity.HasIndex(d => d.DoctorNumber).IsUnique();
                entity.HasIndex(d => d.LicenseNumber).IsUnique();
                entity.HasIndex(d => d.UserId).IsUnique();
            });
        }


        private void ConfigurePatient(ModelBuilder builder)
        {
            builder.Entity<Patient>(entity =>
            {
                entity.ToTable("Patients");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.PatientNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(p => p.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(p => p.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(p => p.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(p => p.NationalId)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(p => p.Gender)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(p => p.CreatedAt)
                    .IsRequired();

                entity.Property(p => p.IsArchived)
                    .HasDefaultValue(false);


                entity.HasOne<ApplicationUser>()
                    .WithOne(u => u.Patient)
                    .HasForeignKey<Patient>(p => p.UserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);


                entity.HasIndex(p => p.PatientNumber).IsUnique();
                entity.HasIndex(p => p.NationalId).IsUnique();
                entity.HasIndex(p => p.UserId).IsUnique();
            });
        }


        private void ConfigureAppointment(ModelBuilder builder)
        {
            builder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointments");

                entity.HasKey(a => a.Id);

                entity.Property(a => a.AppointmentDate).IsRequired();

                entity.Property(a => a.Status).IsRequired();


                entity.HasOne(a => a.Doctor)
                    .WithMany(d => d.Appointments)
                    .HasForeignKey(a => a.DoctorId)
                    .OnDelete(DeleteBehavior.Restrict);


                entity.HasOne(a => a.Patient)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(a => a.PatientId)
                    .OnDelete(DeleteBehavior.Cascade);


                entity.HasIndex(a => a.AppointmentDate);

                entity.HasIndex(a => new { a.DoctorId, a.AppointmentDate });
            });
        }


        private void ConfigureDepartment(ModelBuilder builder)
        {
            builder.Entity<Department>(entity =>
            {
                entity.ToTable("Departments");

                entity.HasKey(d => d.Id);

                entity.Property(d => d.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(d => d.Name).IsUnique();
            });
        }


        private void ConfigureSpecialization(ModelBuilder builder)
        {
            builder.Entity<Specialization>(entity =>
            {
                entity.ToTable("Specializations");

                entity.HasKey(s => s.Id);

                entity.Property(s => s.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(s => s.Name).IsUnique();
            });
        }
    }
}
