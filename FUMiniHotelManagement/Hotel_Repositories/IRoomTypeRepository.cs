using Hotel_BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Repositories
{
    public interface IRoomTypeRepository
    {
        List<RoomType> GetRoomTypes();
    }
}
