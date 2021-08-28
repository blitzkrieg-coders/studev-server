using System;
using System.Collections.Generic;
using System.Linq;

namespace Studev.Server.Models
{
    public class Recruiter
    {
        /// <summary>
        /// User identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Recruiter's email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Hashed password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Recruiter's username
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Recruiter's Company name
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Student candidates marked by the recruiter.
        /// </summary>
        public virtual List<Student> Students { get; set; }


        public Recruiter() { }

    }
}
