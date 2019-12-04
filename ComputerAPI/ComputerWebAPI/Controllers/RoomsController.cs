using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ComputerWebAPI.Models;
using System.Data.SqlClient;

namespace ComputerWebAPI.Controllers
{
    public class RoomsController : ApiController
    {
        private RoomsEntities db = new RoomsEntities();

        // GET: api/Rooms       
        
        public IEnumerable<Room> GetRooms()
        {

            SqlConnection connection = new SqlConnection("Server=tcp:civapi.database.windows.net,1433;Initial Catalog=civapi;User ID=civ_user;Password=Monday1330;");
            string query = "Select * from Room";
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

        /*
        public IEnumerable<Room> GetRoomByUsed(string stauts)
        {
            if( stauts == "computers")
            {
                SqlConnection connection = new SqlConnection("Server=tcp:civapi.database.windows.net,1433;Initial Catalog=civapi;User ID=civ_user;Password=Monday1330;");
                string query = "Select distinct Room.Building, Room.RoomNo, Room.Capacity from Room left Join Computer on Room.RoomNo = Computer.RoomNo AND Room.Building = Computer.Building Where Computer.RoomNo is NOT null;";
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
            
            else if(stauts == "used")
            {
                SqlConnection connection = new SqlConnection("Server=tcp:civapi.database.windows.net,1433;Initial Catalog=civapi;User ID=civ_user;Password=Monday1330;");
                string query = "Select distinct Room.Building, Room.RoomNo, Room.Capacity from Room left Join Class on Room.RoomNo = Class.RoomNo AND Room.Building = CLass.Building Where Class.RoomNo is not null; ";
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

            else if(stauts == "unused")
            {
                SqlConnection connection = new SqlConnection("Server=tcp:civapi.database.windows.net,1433;Initial Catalog=civapi;User ID=civ_user;Password=Monday1330;");
                string query = "Select distinct Room.Building, Room.RoomNo, Room.Capacity from Room left Join Class on Room.RoomNo = Class.RoomNo AND Room.Building = CLass.Building Where Class.RoomNo is not null; ";
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

            else
            {
                return db.Rooms;
            }
            
        }*/
        
        // PUT: api/Rooms/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRoom(string id, Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != room.Building)
            {
                return BadRequest();
            }

            db.Entry(room).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Rooms
        [ResponseType(typeof(Room))]
        public async Task<IHttpActionResult> PostRoom(Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rooms.Add(room);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RoomExists(room.Building))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = room.Building }, room);
        }

        // DELETE: api/Rooms/5
        [ResponseType(typeof(Room))]
        public async Task<IHttpActionResult> DeleteRoom(string id)
        {
            Room room = await db.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            db.Rooms.Remove(room);
            await db.SaveChangesAsync();

            return Ok(room);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoomExists(string id)
        {
            return db.Rooms.Count(e => e.Building == id) > 0;
        }
    }
}