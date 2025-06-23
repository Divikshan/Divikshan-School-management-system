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
            if (string.IsNullOrWhiteSpace(user.Username) ||
                string.IsNullOrWhiteSpace(user.Password) ||
                string.IsNullOrWhiteSpace(user.Role))
            {
                return false;
            }

            if (userRepo.IsUsernameExists(user.Username))
            {
                return false; // Username already exists
            }

            return userRepo.AddUser(user);
        }

        public bool UpdateUser(Users user)
        {
            if (user.UserID <= 0)
                return false;

            if (string.IsNullOrWhiteSpace(user.Username) ||
                string.IsNullOrWhiteSpace(user.Password) ||
                string.IsNullOrWhiteSpace(user.Role))
            {
                return false;
            }

            if (userRepo.IsUsernameExists(user.Username, user.UserID))
            {
                return false; // Username conflict
            }

            return userRepo.UpdateUser(user);
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
