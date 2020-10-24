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
    public class StaffsController : ApiController
    {

        // Instantiate the Database
        private VR_CDEntities db = new VR_CDEntities();

        // GET: api/Staffs
        // Create a message request to access the Staff Model
        public IQueryable<Staff> GetStaffs()
        {
            // Return the results of the data table
            return db.Staffs;
        }

        // GET: api/Staffs/5
        // Create a request to find and return a data field based off the input
        [ResponseType(typeof(Staff))]
        public IHttpActionResult GetStaff(int id)
        {
            // Handle the find function and allocate to a variable
            Staff staff = db.Staffs.Find(id);
            // If the output is null, return an error message
            if (staff == null)
            {
                return NotFound();
            }
            // Return the find output
            return Ok(staff);
        }

        // PUT: api/Staffs/5
        // Create a request to update a data field based off the found result and the id
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStaff(int id, Staff staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != staff.StaffID)
            {
                return BadRequest();
            }
            // Modify the datafield
            db.Entry(staff).State = EntityState.Modified;
            // Try to save the changes to the database
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            // Return a successful status code with no annotations
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Staffs
        // Create a request to add a new data field to the database
        [ResponseType(typeof(Staff))]
        public IHttpActionResult PostStaff(Staff staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Add the data field
            db.Staffs.Add(staff);
            // Try to save the changes
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (StaffExists(staff.StaffID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            // Create a route mapping for the added data field
            return CreatedAtRoute("DefaultApi", new { id = staff.StaffID }, staff);
        }

        // DELETE: api/Staffs/5
        // Create a request to delete a data field from the database
        [ResponseType(typeof(Staff))]
        public IHttpActionResult DeleteStaff(int id)
        {
            // Create a variable to handle the find result
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return NotFound();
            }
            // Remove the data field from the database
            db.Staffs.Remove(staff);
            // Save the changes 
            db.SaveChanges();
            // Return an OK status code
            return Ok(staff);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StaffExists(int id)
        {
            return db.Staffs.Count(e => e.StaffID == id) > 0;
        }
    }
}