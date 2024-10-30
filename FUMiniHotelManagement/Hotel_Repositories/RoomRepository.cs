using Hotel_BussinessObjects;
using Hotel_DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Repositories
{
    public class RoomRepository : IRoomRepository
    {
        public bool CreateRoom(RoomInformation room) => RoomDAO.CreateRoom(room);

        public void DeleteRoom(RoomInformation room) => RoomDAO.DeleteRoom(room);

        public RoomInformation GetRoomById(int id) => RoomDAO.GetRoomById(id);

        public List<RoomInformation> GetRoomInformations() => RoomDAO.GetRoomInformations();

        public void SaveRoom(RoomInformation room) => RoomDAO.SaveRoom(room);

        public void UpdateRoom(RoomInformation room) => RoomDAO.UpdateRoom(room);
    }
}
