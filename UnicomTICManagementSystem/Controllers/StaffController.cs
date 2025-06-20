using System.Collections.Generic;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Repositories;

namespace UnicomTICManagementSystem.Controllers
{
    public class StaffController
    {
        private StaffRepository staffRepository;

        public StaffController()
        {
            staffRepository = new StaffRepository();
        }

        public void AddStaff(Staff staff)
        {
            staffRepository.AddStaff(staff);
        }

        public void UpdateStaff(Staff staff)
        {
            staffRepository.UpdateStaff(staff);
        }

        public void DeleteStaff(int id)
        {
            staffRepository.DeleteStaff(id);
        }

        public List<Staff> GetAllStaffs()
        {
            return staffRepository.GetAllStaff();
        }
    }
}
