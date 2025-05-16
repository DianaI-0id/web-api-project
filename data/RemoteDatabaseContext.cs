using data.DAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data
{
    public class RemoteDatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=45.67.56.214;Port=5454;Password=FY1rOnvu;Username=user10;Database=user10");
        }

        public DbSet<GroupDAO> Groups { get; set; }
        public DbSet<SubjectDAO> Subjects { get; set; }
        public DbSet<StudentsDAO> Students { get; set; }
        public DbSet<GroupSubjectDAO> GroupSubjects { get; set; }
        public DbSet<AttendanceMarkDAO> AttendanceMarks { get; set; }
        public DbSet<AttendanceDAO> Attendances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupDAO>().HasKey(it => it.Id);
            modelBuilder.Entity<SubjectDAO>().HasKey(it => it.Id);
            modelBuilder.Entity<StudentsDAO>().HasKey(it => it.Id);
            modelBuilder.Entity<GroupSubjectDAO>().HasKey(it => it.Id);
            modelBuilder.Entity<AttendanceMarkDAO>().HasKey(it => it.Id);
            modelBuilder.Entity<AttendanceDAO>().HasKey(it => it.Id);

            modelBuilder.Entity<GroupDAO>().Property(it => it.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<SubjectDAO>().Property(it => it.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<StudentsDAO>().Property(it => it.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<GroupSubjectDAO>().Property(it => it.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<AttendanceMarkDAO>().Property(it => it.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<AttendanceDAO>().Property(it => it.Id).ValueGeneratedOnAdd();
        }

    }
}
