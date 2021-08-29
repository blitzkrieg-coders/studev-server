using Newtonsoft.Json.Linq;
using Studev.Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studev.Server.Features.Users
{
    public class GetUser
    {
        private ApiService apirequest;

        /// <summary>
        /// Returns an array with specified user data
        /// </summary>
        /// <param name="username"> User's username on github</param>
        public async Task<JArray> GetUsers(string username) => await apirequest.GetArray($"users/{username}");
    }
}
