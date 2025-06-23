using System.Collections.Generic;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Repositories;

namespace UnicomTICManagementSystem.Controllers
{
    public class StaffController
    {
        private StaffRepository staffRepo = new StaffRepository();

        public List<Staff> GetAllStaffs()
        {
            return staffRepo.GetAllStaff();
        }

        public bool AddStaff(Staff staff)
        {
            // Check for existing username
            var existing = staffRepo.GetAllStaff().Find(s => s.Username.ToLower() == staff.Username.ToLower());
            if (existing != null) return false;

            staffRepo.AddStaff(staff);
            return true;
        }

        public void UpdateStaff(Staff staff)
        {
            staffRepo.UpdateStaff(staff);
        }

        public void DeleteStaff(int staffId)
        {
            staffRepo.DeleteStaff(staffId);
        }
    }
}
