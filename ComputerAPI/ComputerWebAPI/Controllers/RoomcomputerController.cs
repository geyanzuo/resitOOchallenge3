using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ComputerWebAPI.Models;
using System.Data.SqlClient;

namespace ComputerWebAPI.Controllers
{
    public class RoomcomputerController : ApiController
    {
       
        // GET: api/Roomcomputer
        public IEnumerable<Room> Get()
        {
            SqlConnection connection = new SqlConnection("Server=tcp:civapi.database.windows.net,1433;Initial Catalog=civapi;User ID=civ_user;Password=Monday1330;");
            string query = "Select distinct Room.Building, Room.RoomNo, Room.Capacity from Room left Join Computer on Room.RoomNo = Computer.RoomNo AND Room.Building = Computer.Building Where Computer.RoomNo is NOT null;";
            SqlCommand com = new SqlCommand(query,connection);
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

        /*
        // GET: api/Roomcomputer/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Roomcomputer
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Roomcomputer/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Roomcomputer/5
        public void Delete(int id)
        {
        }
        */
    }
}
