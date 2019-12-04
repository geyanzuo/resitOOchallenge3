using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using ComputerWebAPI.Models;

namespace ComputerWebAPI.Controllers
{
    public class RoomUnusedController : ApiController
    {
        // GET: api/RoomUsed
        public IEnumerable<Room> GetUnusedRoom()
        {
            SqlConnection connection = new SqlConnection("Server=tcp:civapi.database.windows.net,1433;Initial Catalog=civapi;User ID=civ_user;Password=Monday1330;");
            string query = "Select distinct Room.Building, Room.RoomNo, Room.Capacity, Class.ClassCode from Room left Join Class on Room.RoomNo = Class.RoomNo AND Room.Building = Class.Building Where Class.RoomNo is null;";
            SqlCommand com = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader result = com.ExecuteReader();

            List<Room> rooms = new List<Room>();
            while (result.Read())
            {
                rooms.Add(new Room(result[0].ToString(), int.Parse(result[1].ToString()), int.Parse(result[2].ToString())));
            }

            connection.Close();
            return rooms;
        }

        // GET: api/RoomUsed/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/RoomUsed
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/RoomUsed/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/RoomUsed/5
        public void Delete(int id)
        {
        }
    }
}
