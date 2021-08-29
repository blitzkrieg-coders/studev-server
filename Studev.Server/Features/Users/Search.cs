using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;

using Studev.Server.Database;
using Studev.Server.Services;

namespace Studev.Server.Features.Users {
    public class Search {
        public record StudentDto {
            public string GitHubLogin { get; init; }
            public string Name { get; init; }
            public string AvatarUrl { get; init; }
            public int RepositoriesAmount { get; set; }
        }

        public record Query : IRequest<IEnumerable<StudentDto>> {
            public string Language { get; init; }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<StudentDto>> {
            private readonly StudevContext _context;
            private readonly ApiService _apiService;

            public Handler(StudevContext context, ApiService apiService) {
                _context = context;
                _apiService = apiService;
            }

            public async Task<IEnumerable<StudentDto>> Handle(Query request, CancellationToken cancellationToken) {
                var students = await _context.Students
                    .Select(s => new StudentDto {
                        GitHubLogin = s.GitHubLogin
                        //, Name = s.Name
                    })
                    .ToArrayAsync(cancellationToken);

                foreach (var student in students) {
                    var repos = await _apiService.GetArray($"https://api.github.com/users/{student.GitHubLogin}/repos");
                    student.RepositoriesAmount = repos
                        .Where(r => !(bool)r["fork"])
                        .GroupBy(r => r["language"].ToString())
                        .Where(g => g.Key == request.Language)
                        .Select(g => g.Count())
                        .SingleOrDefault();
                }

                return students.Where(s => s.RepositoriesAmount > 0);
            }
        }
    }
}
