using System.Collections.Generic;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Studev.Server.Features.Users {
    [Route("api/users")]
    [ApiController]
    [AllowAnonymous]
    public class Controller : ControllerBase {
        private readonly IMediator _mediator;

        public Controller(IMediator mediator) {
            _mediator = mediator;

        }

        [HttpPost("auth/signup")]
        public async Task<ActionResult<int>> SignUp([FromBody] Add.Command request) {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost("auth/login")]
        public async Task<ActionResult<LogIn.StudentSession>> LogIn(int gitHubId) {
            var student = await _mediator.Send(new LogIn.Query {
                GitHubId = gitHubId
            });

            return student is null ? NotFound() : Ok(student);
        }

        [HttpGet("details/{gitHubLogin}")]
        public async Task<ActionResult<Details.StudentData>> GetDetails(string gitHubLogin) {
            var studentData = await _mediator.Send(new Details.Query {
                GitHubLogin = gitHubLogin
            });

            return studentData is null ? NotFound() : Ok(studentData);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Search.StudentMatch>>> SearchBy([FromQuery] string language) {
            var students = await _mediator.Send(new Search.Query {
                Language = language
            });

            return Ok(students);
        }
    }
}
