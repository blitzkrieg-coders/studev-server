using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using Studev.Server.Database;

namespace Studev.Server.Features.Users {
    public class LogIn {
        public record StudentSession {
            public int GitHubId { get; init; }
            public string GitHubLogin { get; init; }
            public string Email { get; init; }
            public string Token { get; set; }
        }

        public record Query : IRequest<StudentSession> {
            public int GitHubId { get; init; }
        }

        public class Handler : IRequestHandler<Query, StudentSession> {
            private readonly IConfiguration _configuration;
            private readonly StudevContext _context;

            public Handler(IConfiguration configuration, StudevContext context) {
                _configuration = configuration;
                _context = context;
            }

            public async Task<StudentSession> Handle(Query request, CancellationToken cancellationToken) {
                var studentDto = await _context.Students
                    .Where(s => s.GitHubId == request.GitHubId)
                    .Select(s => new StudentSession {
                        GitHubId = s.GitHubId,
                        GitHubLogin = s.GitHubLogin,
                        Email = s.Email
                    })
                    .SingleOrDefaultAsync(cancellationToken);

                if (studentDto is null) {
                    return null;
                }

                studentDto.Token = new JwtSecurityTokenHandler()
                    .WriteToken(new JwtSecurityToken(
                        expires: DateTime.Now.AddDays(7),
                        claims: new[] {
                            new Claim(JwtRegisteredClaimNames.Sub, studentDto.GitHubLogin),
                            new Claim(JwtRegisteredClaimNames.Email, studentDto.Email)
                        },
                        signingCredentials: new SigningCredentials(
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(
                                    _configuration.GetValue<string>("Key"))),
                            SecurityAlgorithms.HmacSha256)));
                return studentDto;
            }
        }
    }
}
