using System.Collections.Generic;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Repositories;

namespace UnicomTICManagementSystem.Controllers
{
    public class SubjectController
    {
        private SubjectRepository repo = new SubjectRepository();

        public void AddSubject(Subject s) => repo.AddSubject(s);
        public void UpdateSubject(Subject s) => repo.UpdateSubject(s);
        public void DeleteSubject(int id) => repo.DeleteSubject(id);
        public List<Subject> GetAllSubjects() => repo.GetAllSubjects();
    }
}
