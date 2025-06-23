using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicomTICManagementSystem.Models
{
    public class Lecturer
    {
        public int LecturerID { get; set; }         // Primary Key (from Users table)
        public string Name { get; set; }        // Display name (can be same as Username or separately stored)
        public string Username { get; set; }    // Login username
        public string Password { get; set; }    // Login password (in production, use hashing)
        public string Role { get; set; } = "Lecturer";
    }
}
