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
    public class ComputersController : ApiController
    {
        private RoomsEntities db = new RoomsEntities();

        // GET: api/Computer
        public IEnumerable<Computer> GetComputers()
        {
            SqlConnection connection = new SqlConnection("Server=tcp:civapi.database.windows.net,1433;Initial Catalog=civapi;User ID=civ_user;Password=Monday1330;");
            string query = "Select *From Computer";
            SqlCommand com = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader result = com.ExecuteReader();

            List<Computer> computer = new List<Computer>();
            while (result.Read())
            {
                computer.Add(new Computer(int.Parse(result[0].ToString()), int.Parse(result[1].ToString()),result[3].ToString(), int.Parse(result[4].ToString())));
            }

            connection.Close();
            return computer;
        }

        // GET: api/Computer/5
        public IEnumerable<Computer> GetComputers(string id)
        {
            SqlConnection connection = new SqlConnection("Server=tcp:civapi.database.windows.net,1433;Initial Catalog=civapi;User ID=civ_user;Password=Monday1330;");
            string query = "Select * From Computer where @id = ClassCode";
            SqlCommand com = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader result = com.ExecuteReader();

            List<Computer> computer = new List<Computer>();
            while (result.Read())
            {
                computer.Add(new Computer(int.Parse(result[0].ToString()), int.Parse(result[1].ToString()), result[3].ToString(), int.Parse(result[4].ToString())));
            }

            connection.Close();
            return computer;
        }

        // PUT: api/Computer/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutComputer(int id, Computer computer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != computer.Number)
            {
                return BadRequest();
            }

            db.Entry(computer).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComputerExists(id))
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

        // POST: api/Computer
        [ResponseType(typeof(Computer))]
        public async Task<IHttpActionResult> PostComputer(Computer computer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Computers.Add(computer);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ComputerExists(computer.Number))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = computer.Number }, computer);
        }

        // DELETE: api/Computer/5
        [ResponseType(typeof(Computer))]
        public async Task<IHttpActionResult> DeleteComputer(int id)
        {
            Computer computer = await db.Computers.FindAsync(id);
            if (computer == null)
            {
                return NotFound();
            }

            db.Computers.Remove(computer);
            await db.SaveChangesAsync();

            return Ok(computer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ComputerExists(int id)
        {
            return db.Computers.Count(e => e.Number == id) > 0;
        }
    }
}