using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Studev.Server.Database;
using Studev.Server.Models;

namespace Studev.Server.Features.Users {
    public class Add {
        public record Command : IRequest<int> {
            public Student Student { get; init; }
        }

        public class Handler : IRequestHandler<Command, int> {
            private readonly StudevContext _context;

            public Handler(StudevContext context) {
                _context = context;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken) {
                request.Student.Id = 0;
                _context.Students.Add(request.Student);
                await _context.SaveChangesAsync(cancellationToken);
                return request.Student.Id;
            }
        }
    }
}
