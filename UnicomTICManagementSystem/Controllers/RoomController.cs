using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Repositories;

namespace UnicomTICManagementSystem.Controllers
{
    internal class RoomController
    {
        private readonly RoomRepository _roomRepository;

        public RoomController()
        {
            _roomRepository = new RoomRepository();
        }

        public List<Room> GetAllRooms()
        {
            return _roomRepository.GetAllRooms();
        }

        public bool AddRoom(Room room)
        {
            return _roomRepository.AddRoom(room); // ✅ Now returns success/failure
        }

        public bool UpdateRoom(Room room)
        {
            return _roomRepository.UpdateRoom(room); // ✅ Now returns success/failure
        }

        public bool DeleteRoom(int roomId)
        {
            return _roomRepository.DeleteRoom(roomId); // ✅ Now returns success/failure
        }

        public Room GetRoomById(int roomId)
        {
            return _roomRepository.GetRoomById(roomId);
        }
    }
}

