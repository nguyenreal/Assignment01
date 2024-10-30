﻿using Hotel_BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_DAOs
{
    public class RoomTypeDAO
    {
        public static List<RoomType> GetRoomTypes()
        {
            var listRoomTypes = new List<RoomType>();
            try
            {
                using var context = new FuminiHotelManagementContext();
                listRoomTypes = context.RoomTypes.ToList();
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listRoomTypes;
        }
    }
}
