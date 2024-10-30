using Hotel_BussinessObjects;
using Hotel_DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Repositories
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        public List<RoomType> GetRoomTypes() => RoomTypeDAO.GetRoomTypes();
    }
}
