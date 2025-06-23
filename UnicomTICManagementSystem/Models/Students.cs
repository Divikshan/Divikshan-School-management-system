using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicomTICManagementSystem.Models
{
    public class Students
    {
        public int StudentID { get; set; }  // Primary key
        public string StudentName { get; set; }
        public string Username { get; set; }  // For login
        public string Password { get; set; }  // For login (hash recommended in real app)
        public int CourseID { get; set; }      // Foreign key to Course
                                               // Add other fields as needed (email, etc.)
        
    }
}
        