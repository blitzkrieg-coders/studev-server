using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Studev.Server.Services;

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

        [HttpPost("auth/signup")]
        public async Task<ActionResult<int>> SignUp([FromBody] Add.Command request) {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost("auth/login")]
        public async Task<ActionResult<Get.StudentDto>> LogIn(int gitHubId) {
            var student = await _mediator.Send(new Get.Query {
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

        //gets profile of user's github username
        //public async Task<JObject> GetProfile(string username)
        //{
        //    var profile = await apiService.GetObject($"users/{username}");
        //    return profile;
        //}

        ////get repos of specified user's github username
        //public async Task<JArray> GetUserRepos(string username)
        //{
        //    var repos = await apiService.GetArray($"users/{username}/repos?type=owner");
        //    return repos;
        //}

        ////get repo languages given a github username and repo name
        //public async Task<JObject> GetRepoLanguages(string username, string reponame)
        //{
        //    var repos = await apiService.GetObject($"repos/{username}/{reponame}/languages");
        //    return repos;
        //}
    }
}
