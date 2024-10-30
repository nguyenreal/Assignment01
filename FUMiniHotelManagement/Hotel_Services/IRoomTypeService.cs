using Hotel_BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Services
{
    public interface IRoomTypeService
    {
        List<RoomType> GetRoomTypes();
    }
}
