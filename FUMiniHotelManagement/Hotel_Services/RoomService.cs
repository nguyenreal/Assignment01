using Hotel_BussinessObjects;
using Hotel_Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository iRoomRepository;
        public RoomService()
        {
            iRoomRepository = new RoomRepository();
        }

        public bool CreateRoom(RoomInformation room)
        {
            return iRoomRepository.CreateRoom(room);
        }

        public void DeleteRoom(RoomInformation room)
        {
            iRoomRepository.DeleteRoom(room);
        }

        public RoomInformation GetRoomById(int id)
        {
            return iRoomRepository.GetRoomById(id);
        }

        public List<RoomInformation> GetRoomInformations()
        {
            return iRoomRepository.GetRoomInformations();
        }

        public void SaveRoom(RoomInformation room)
        {
            iRoomRepository.SaveRoom(room);
        }

        public void UpdateRoom(RoomInformation room)
        {
            iRoomRepository.UpdateRoom(room);
        }
    }
}
