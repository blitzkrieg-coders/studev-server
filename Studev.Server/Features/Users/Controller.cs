using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Studev.Server.Features.Users {
    [Route("api/users")]
    [ApiController]
    [AllowAnonymous]
    public class Controller : ControllerBase {
        private readonly IMediator _mediator;

        public Controller(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> SignUp([FromBody] Add.Command request) {
            return Ok(await _mediator.Send(request));
        }
    }
}
