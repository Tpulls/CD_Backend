using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CD_Backend.Models;

namespace CD_Backend.Controllers
{
    // Create a CDsController class that derives from the ApiController 
    public class CDsController : ApiController
    {
        // Initialize the Database
        private VR_CDEntities db = new VR_CDEntities();

        // GET: api/CDs
        // Calls a method that retrieves the data stored in the parameter
        public IQueryable<CD> GetCDs()
        {
            // Return findings to the CDsController
            return db.CDs;
        }

        // GET: api/CDs/5
        // Creates a message request to retrieve information based off the parameter(id)
        // Response Type sets the data type to suit the CD data fields
        [ResponseType(typeof(CD))]
        public IHttpActionResult GetCD(int id)
        {
            // Find the corresponding data in the CD DB according to id
            CD cD = db.CDs.Find(id);
            if (cD == null)
            {
                // If cD has no value, return not found
                return NotFound();
            }
            // Return the findings of the find
            return Ok(cD);
        }

        // PUT: api/CDs/5
        // Creates a message request to update information based of the parameters(id & cD)
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCD(int id, CD cD)
        {
            // Check if the state of the model is working
            if (!ModelState.IsValid)
            {
                // Return an error message
                return BadRequest(ModelState);
            }

            // Check if the id matches the found data
            if (id != cD.cdID)
            {
                // Return an error message
                return BadRequest();
            }
            // Set the DB state to modifiable
            db.Entry(cD).State = EntityState.Modified;
            // Try to update the data
            try
            {
                // Save the database
                db.SaveChanges();
            }
            // Catch any error
            catch (DbUpdateConcurrencyException)
            {
                if (!CDExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            // Return statuscode successful with no remark
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CDs
        // Creates a message request to add information based of the parameter(cD)
        [ResponseType(typeof(CD))]
        public IHttpActionResult PostCD(CD cD)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Add the parameter to the databae
            db.CDs.Add(cD);
            // save the changes
            db.SaveChanges();

            // Returns the route for the newly created data field
            return CreatedAtRoute("DefaultApi", new { id = cD.cdID }, cD);
        }

        // DELETE: api/CDs/5
        // Creates a message request to delete data based off the parameter(id)
        [ResponseType(typeof(CD))]
        public IHttpActionResult DeleteCD(int id)
        {
            // Find the data field relative to the input
            CD cD = db.CDs.Find(id);
            // If null, return error
            if (cD == null)
            {
                return NotFound();
            }
            // Remove the data field from the database
            db.CDs.Remove(cD);
            // Save the database
            db.SaveChanges();
            // Return an OK status code
            return Ok(cD);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CDExists(int id)
        {
            return db.CDs.Count(e => e.cdID == id) > 0;
        }
    }
}