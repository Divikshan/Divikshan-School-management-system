using System.Collections.Generic;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Repositories;

namespace UnicomTICManagementSystem.Controllers
{
    public class CourseController
    {
        private CourseRepository courseRepo = new CourseRepository();

        public void AddCourse(Course course) => courseRepo.AddCourse(course);
        public void UpdateCourse(Course course) => courseRepo.UpdateCourse(course);
        public void DeleteCourse(int id) => courseRepo.DeleteCourse(id);
        public List<Course> GetAllCourses() => courseRepo.GetAllCourses();
    }
}
