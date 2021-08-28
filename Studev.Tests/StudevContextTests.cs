using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using Studev.Server.Database;
using Studev.Server.Models;

using Xunit;

namespace Studev.Tests {
    public class StudevContextTests {
        public DbContextOptions<StudevContext> Options { get; }

        public StudevContextTests() {
            var connection = new SqliteConnection("Mode=memory");
            connection.Open();

            Options = new DbContextOptionsBuilder<StudevContext>()
                .UseSqlite(connection)
                .Options;
        }

        [Fact]
        public async Task Add_RecruiterWithStudents_SavesAll() {
            using var context = new StudevContext(Options);

            await context.Database.EnsureCreatedAsync();

            var recruiter = new Recruiter {
                Name = "Reclutador",
                Students = new List<Student> {
                    new Student {
                        GithubLogin = "username"
                    },
                    new Student {
                        GithubLogin = "octocat"
                    }
                }
            };

            context.Recruiters.Add(recruiter);
            await context.SaveChangesAsync();

            var actualStudent = await context.Students
                .Where(s => s.GithubLogin == "username")
                .FirstOrDefaultAsync();
            Assert.Equal("Reclutador", actualStudent.Recruiters[0].Name);
        }
    }
}
