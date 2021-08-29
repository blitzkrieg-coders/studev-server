using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Studev.Server.Database;
using Studev.Server.Services;

namespace Studev.Server.Features.Users {
    public class Details {
        public record StudentData {
            public record RepositoryStats {
                public string Language { get; init; }
                public int Amount { get; init; }
                public int Commits { get; init; }
            }

            public string GitHubLogin { get; init; }
            public string Name { get; init; }
            public string AvatarUrl { get; init; }
            public string Biography { get; init; }
            public string Location { get; init; }
            public DateTime StudyStart { get; init; }
            public DateTime StudyEnd { get; init; }
            public string Career { get; init; }
            public string School { get; init; }
            public List<RepositoryStats> RepositoriesStats { get; set; }
            public int TotalRepositories => RepositoriesStats.Sum(r => r.Amount);
            public int TotalCommits => RepositoriesStats.Sum(r => r.Commits);
        }

        public record Query : IRequest<StudentData> {
            public string GitHubLogin { get; init; }
        }

        public class Handler : IRequestHandler<Query, StudentData> {
            private readonly StudevContext _context;
            private readonly ApiService _apiService;
            private readonly ILogger<Handler> _logger;

            public Handler(StudevContext context, ApiService apiService, ILogger<Handler> logger) {
                _context = context;
                _apiService = apiService;
                _logger = logger;
            }

            public async Task<StudentData> Handle(Query request, CancellationToken cancellationToken) {
                var user = await _apiService.GetObject($"https://api.github.com/users/{request.GitHubLogin}");
                if (user is null) {
                    return null;
                }
                var studentData = await _context.Students
                    .Where(s => s.GitHubLogin == request.GitHubLogin)
                    .Select(s => new StudentData {
                        GitHubLogin = s.GitHubLogin,
                        Name = user["name"].ToString(),
                        AvatarUrl = user["avatar_url"].ToString(),
                        Biography = user["bio"].ToString(),
                        Location = user["location"].ToString(),
                        StudyStart = s.StudyStart,
                        StudyEnd = s.StudyEnd,
                        Career = s.Career,
                        School = s.School
                    })
                    .SingleOrDefaultAsync(cancellationToken);
                if (studentData is null) {
                    return null;
                }

                var repos = await _apiService.GetArray(user["repos_url"].ToString());
                var studentRepos = new List<StudentData.RepositoryStats>();
                foreach (var repo in repos.Where(r => !(bool)r["fork"])) {
                    var language = repo["language"].ToString();
                    if (language == "") {
                        continue;
                    }

                    var contributors = await _apiService.GetArray(repo["contributors_url"].ToString());
                    var contributor = contributors
                        .FirstOrDefault(c => c["login"].ToString() == request.GitHubLogin);
                    studentRepos.Add(new StudentData.RepositoryStats {
                        Language = language,
                        Commits = (int)contributor["contributions"]
                    });
                }

                studentData.RepositoriesStats = studentRepos
                    .GroupBy(r => r.Language)
                    .Select(g => new StudentData.RepositoryStats {
                        Language = g.Key,
                        Amount = g.Count(),
                        Commits = g.Sum(r => r.Commits)
                    })
                    .ToList();

                return studentData;
            }
        }
    }
}
