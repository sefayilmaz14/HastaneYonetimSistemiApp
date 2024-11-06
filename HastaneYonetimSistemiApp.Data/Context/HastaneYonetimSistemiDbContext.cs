using HastaneYonetimSistemiApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneYonetimSistemiApp.Data.Context
{
    public class HastaneYonetimSistemiDbContext : DbContext
    {
        public HastaneYonetimSistemiDbContext(DbContextOptions<HastaneYonetimSistemiDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent Api

            modelBuilder.ApplyConfiguration(new AppointmentConfiguration());
            modelBuilder.ApplyConfiguration(new DoctorAppointmentConfiguration());
            modelBuilder.ApplyConfiguration(new DoctorConfiguration());
            modelBuilder.ApplyConfiguration(new PatientAppointmentConfiguration());
            modelBuilder.ApplyConfiguration(new PatientConfiguration());
            modelBuilder.ApplyConfiguration(new PoliclinicConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.Entity<SettingEntity>().HasData(
                new SettingEntity
                {
                    Id = 1,
                    MaintenenceMode = false
                });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AppointmentsEntity> Appointments => Set<AppointmentsEntity>();
        public DbSet<DoctorAppointmentEntity> DoctorAppointments => Set<DoctorAppointmentEntity>();
        public DbSet<DoctorEntity> Doctors => Set<DoctorEntity>();
        public DbSet<PatientAppointmentEntity> PatientAppointments => Set<PatientAppointmentEntity>();
        public DbSet<PatientEntity> Patients => Set<PatientEntity>();
        public DbSet<PoliclinicEntity> Policlinics => Set<PoliclinicEntity>();
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<SettingEntity> Setting => Set<SettingEntity>();
    }
}
