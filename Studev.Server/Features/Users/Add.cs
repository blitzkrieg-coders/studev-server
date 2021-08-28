using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Studev.Server.Database;
using Studev.Server.Models;

namespace Studev.Server.Features.Users {
    public class Add {
        public record Command : IRequest<int> {
            public string GitHubLogin { get; init; }
            public string Email { get; init; }
            public string HashedPassword { get; init; }
        }

        public class Handler : IRequestHandler<Command, int> {
            private readonly StudevContext _context;

            public Handler(StudevContext context) {
                _context = context;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken) {
                var student = new Student {
                    GithubLogin = request.GitHubLogin,
                    Email = request.Email,
                    Password = request.HashedPassword
                };
                _context.Students.Add(student);
                await _context.SaveChangesAsync(cancellationToken);
                return student.Id;
            }
        }
    }
}
