using System.Collections.Generic;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Repositories;

namespace UnicomTICManagementSystem.Controllers
{
    public class UserController
    {
        private readonly UserRepository userRepo = new UserRepository();

        public List<Users> GetAllUsers()
        {
            return userRepo.GetAllUsers();
        }

        public bool AddUser(Users user)
        {
            // Example validation before adding
            if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password) || string.IsNullOrWhiteSpace(user.Role))
                return false;

            // Could add more business rules here

            userRepo.AddUser(user);
            return true;
        }

        public bool UpdateUser(Users user)
        {
            if (user.UserID <= 0)
                return false;

            // Add validation or business logic here if needed

            userRepo.UpdateUser(user);
            return true;
        }

        public bool DeleteUser(int userId)
        {
            if (userId <= 0)
                return false;

            userRepo.DeleteUser(userId);
            return true;
        }
    }
}
