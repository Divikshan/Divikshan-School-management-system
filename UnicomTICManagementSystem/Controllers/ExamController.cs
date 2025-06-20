using System.Collections.Generic;
using System.Data;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Repositories;

namespace UnicomTICManagementSystem.Controllers
{
    public class ExamController
    {
        private ExamRepository examRepo;

        public ExamController()
        {
            examRepo = new ExamRepository();
        }

        public void AddExam(Exam exam)
        {
            examRepo.AddExam(exam);
        }

        public void UpdateExam(Exam exam)
        {
            examRepo.UpdateExam(exam);
        }

        public void DeleteExam(int examId)
        {
            examRepo.DeleteExam(examId);
        }

        public List<Exam> GetAllExams()
        {
            return examRepo.GetAllExams();
        }

        public DataTable GetAllExamsWithSubjectNames()
        {
            return examRepo.GetAllExamsWithSubjectNames();
        }

        public List<Subject> GetAllSubjects()
        {
            var subjectRepo = new SubjectRepository();
            return subjectRepo.GetAllSubjects();
        }
    }
}
