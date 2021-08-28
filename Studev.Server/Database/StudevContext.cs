
using Microsoft.EntityFrameworkCore;

using Studev.Server.Models;

namespace Studev.Server.Database {
    public class StudevContext : DbContext {
        public DbSet<Student> Students { get; set; }
        public DbSet<Recruiter> Recruiters { get; set; }

        public StudevContext(DbContextOptions<StudevContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Student>(student => {
                student.ToTable(nameof(Student));
            });

            modelBuilder.Entity<Recruiter>(recruiter => {
                recruiter.ToTable(nameof(Recruiter));
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
