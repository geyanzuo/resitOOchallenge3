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
    public class ClassController : ApiController
    {
        RoomsEntities db = new RoomsEntities();

        // GET: api/Class
        public IEnumerable<Class> GetClasses()
        {
            SqlConnection connection = new SqlConnection("Server=tcp:civapi.database.windows.net,1433;Initial Catalog=civapi;User ID=civ_user;Password=Monday1330;");
            string query = "Select *From Class";
            SqlCommand com = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader result = com.ExecuteReader();

            List<Class> classes = new List<Class>();
            while (result.Read())
            {
                classes.Add(new Class(result[0].ToString(),  result[1].ToString(), result[2].ToString(), int.Parse(result[3].ToString())));
            }

            connection.Close();
            return classes;
        }
        
        // GET: api/Class/5
        public IEnumerable <Class> Get(string id)
        {
            SqlConnection connection = new SqlConnection("Server=tcp:civapi.database.windows.net,1433;Initial Catalog=civapi;User ID=civ_user;Password=Monday1330;");
            string query = " Select *From Class Where ClassCode =" + id;
            SqlCommand com = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader result = com.ExecuteReader();

            List<Class> classes = new List<Class>();
            while (result.Read())
            {
                classes.Add(new Class(result[0].ToString(), result[1].ToString(), result[2].ToString(), int.Parse(result[3].ToString())));
            }

            connection.Close();
            return classes;
        }

        

        // PUT: api/Class/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutClass(string id, Class @class)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @class.ClassCode)
            {
                return BadRequest();
            }

            db.Entry(@class).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(id))
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

        // POST: api/Class
        [ResponseType(typeof(Class))]
        public async Task<IHttpActionResult> PostClass(Class @class)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Classes.Add(@class);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClassExists(@class.ClassCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = @class.ClassCode }, @class);
        }

        // DELETE: api/Class/5
        [ResponseType(typeof(Class))]
        public async Task<IHttpActionResult> DeleteClass(string id)
        {
            Class @class = await db.Classes.FindAsync(id);
            if (@class == null)
            {
                return NotFound();
            }

            db.Classes.Remove(@class);
            await db.SaveChangesAsync();

            return Ok(@class);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClassExists(string id)
        {
            return db.Classes.Count(e => e.ClassCode == id) > 0;
        }
    }
}