using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;

using Studev.Server.Database;
using Studev.Server.Services;

namespace Studev.Server.Features.Users {
    public class Details {
        public record StudentData {
            public record Repository {
                public string Language { get; init; }
            }

            public string GitHubLogin { get; init; }
            public DateTime StudyStart { get; init; }
            public DateTime StudyEnd { get; init; }
            public string Career { get; init; }
            public string School { get; init; }
            public List<Repository> Repositories { get; set; }
        }

        public record Query : IRequest<StudentData> {
            public string GitHubLogin { get; init; }
        }

        public class Handler : IRequestHandler<Query, StudentData> {
            private readonly StudevContext _context;
            private readonly ApiService _apiService;

            public Handler(StudevContext context, ApiService apiService) {
                _context = context;
                _apiService = apiService;
            }

            public async Task<StudentData> Handle(Query request, CancellationToken cancellationToken) {
                var repos = await _apiService.GetArray($"users/{request.GitHubLogin}/repos?type=owner");

                var studentData = await _context.Students
                    .Where(s => s.GitHubLogin == request.GitHubLogin)
                    .Select(s => new StudentData {
                        GitHubLogin = s.GitHubLogin,
                        StudyStart = s.StudyStart,
                        StudyEnd = s.StudyEnd,
                        Career = s.Career,
                        School = s.School
                    })
                    .SingleOrDefaultAsync();
                if (studentData is null) {
                    return null;
                }

                studentData.Repositories = repos
                    .Select(r => new StudentData.Repository {
                        Language = r["language"].ToString()
                    })
                    .ToList();
                return studentData;
            }
        }
    }
}
