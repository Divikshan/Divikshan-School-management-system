using System.Collections.Generic;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Repositories;

namespace UnicomTICManagementSystem.Controllers
{
    public class StudentController
    {
        private StudentRepository studentRepo = new StudentRepository();

        public void AddStudent(Student student) => studentRepo.AddStudent(student);
        public void UpdateStudent(Student student) => studentRepo.UpdateStudent(student);
        public void DeleteStudent(int id) => studentRepo.DeleteStudent(id);
        public List<Student> GetAllStudents() => studentRepo.GetAllStudents();
    }
}
