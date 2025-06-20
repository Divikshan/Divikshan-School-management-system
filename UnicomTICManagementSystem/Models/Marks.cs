using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicomTICManagementSystem.Models
{
    public class Marks
    {
        public int MarkID { get; set; }
        public int StudentID { get; set; }
        public int SubjectID { get; set; }
        public int ExamID { get; set; }
        public int MarksObtained { get; set; }
    }
}
