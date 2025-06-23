using System.Collections.Generic;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Repositories;

namespace UnicomTICManagementSystem.Controllers
{
    public class StudentController
    {
        private readonly StudentRepository studentRepo = new StudentRepository();

        public void AddStudent(Students student) => studentRepo.AddStudent(student);
        public void UpdateStudent(Students student) => studentRepo.UpdateStudent(student);
        public void DeleteStudent(int id) => studentRepo.DeleteStudent(id);
        public List<Students> GetAllStudents() => studentRepo.GetAllStudents();
    }
}