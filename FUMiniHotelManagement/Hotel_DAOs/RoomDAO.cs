using Hotel_BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_DAOs
{
    public class RoomDAO
    {
        public static List<RoomInformation> GetRoomInformations()
        {
            var listRoomInfo = new List<RoomInformation>();
            try
            {
                using var db = new FuminiHotelManagementContext();
                listRoomInfo = db.RoomInformations.ToList();
            }
            catch (Exception e) { }
            return listRoomInfo;
        }

        public static void SaveRoom(RoomInformation room)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                context.RoomInformations.Add(room);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void DeleteRoom(RoomInformation room)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                var p1 =
                    context.RoomInformations.SingleOrDefault(c => c.RoomId == room.RoomId);
                context.RoomInformations.Remove(p1);

                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void UpdateRoom(RoomInformation room)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                context.Entry<RoomInformation>(room).State
                    = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static RoomInformation GetRoomById(int id)
        {
            using var db = new FuminiHotelManagementContext();
            return db.RoomInformations.FirstOrDefault(c => c.RoomId.Equals(id));
        }

        public static bool CreateRoom(RoomInformation room)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                context.RoomInformations.Add(room);
                context.SaveChanges();
                return true; // Indicates success
            }
            catch (Exception e)
            {
                // You can log the exception message or handle it as needed
                Console.WriteLine("Error: " + e.Message);
                return false; // Indicates failure
            }
        }
    }
}
