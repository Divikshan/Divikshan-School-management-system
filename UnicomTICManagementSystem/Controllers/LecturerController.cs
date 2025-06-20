using System.Collections.Generic;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Repositories;

namespace UnicomTICManagementSystem.Controllers
{
    public class LecturerController
    {
        private LecturerRepository lecturerRepository;

        public LecturerController()
        {
            lecturerRepository = new LecturerRepository();
        }

        public void AddLecturer(Lecturer lecturer)
        {
            lecturerRepository.AddLecturer(lecturer);
        }

        public void UpdateLecturer(Lecturer lecturer)
        {
            lecturerRepository.UpdateLecturer(lecturer);
        }

        public void DeleteLecturer(int lecturerId)
        {
            lecturerRepository.DeleteLecturer(lecturerId);
        }

        public List<Lecturer> GetAllLecturers()
        {
            return lecturerRepository.GetAllLecturers();
        }
    }
}
