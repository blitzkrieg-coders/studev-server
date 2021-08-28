using System;
using System.Collections.Generic;
using System.Linq;

namespace Studev.Server.Models
{
    /// <summary>
    /// Class that represents a Student account.
    /// </summary>
    public class Student
    {
        /// <summary>
        /// User identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Student's Github username
        /// </summary>
        public string GithubLogin { get; set; }

        /// <summary>
        /// Student's email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Hashed password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Datetime object representing the start of the Student´s career
        /// </summary>
        public DateTime StudyStart { get; set; }

        /// <summary>
        /// Datetime object representing the end of the Student´s career
        /// </summary>
        public DateTime StudyEnd { get; set; }

        /// <summary>
        /// Name of the student's career.
        /// </summary>
        public string Career { get; set; }

        /// <summary>
        /// Name of the student's school/university.
        /// </summary>
        public string School { get; set; }


        public User() { }

       
    }
}
