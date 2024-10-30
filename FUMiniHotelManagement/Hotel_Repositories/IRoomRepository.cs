using Hotel_BussinessObjects;
using Hotel_DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Repositories
{
    public interface IRoomRepository
    {
        List<RoomInformation> GetRoomInformations();


        void SaveRoom(RoomInformation room);


        void DeleteRoom(RoomInformation room);

        void UpdateRoom(RoomInformation room);

        RoomInformation GetRoomById(int id);
        public bool CreateRoom(RoomInformation room);
    }
}
