﻿using AttendenceSystem.Confug;
using AttendenceSystem.Models;
using Microsoft.EntityFrameworkCore;

 namespace AttendenceSystem.Data
{
    public class DataContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Employee>Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Intake> Intake { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Programs> Programs { get; set; }
        public DbSet<Permision> Permisions { get; set; }
        public DbSet<Attendence> Attendences { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<InstructorTrack> instructorTracks { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer("DefaultConnection");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new InstructorConfig());
            modelBuilder.ApplyConfiguration(new UserConfug());
            modelBuilder.ApplyConfiguration(new InstructorTrackConfig());

            //modelBuilder.ApplyConfiguration(new TrackConfig());

            modelBuilder.Entity<User>(user =>
            user.UseTptMappingStrategy()
            );

        }

    }
}
