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
    public class RentalsController : ApiController
    {
        // Instantiate the Database
        private VR_CDEntities db = new VR_CDEntities();

        // GET: api/Rentals
        // Create a method to request a datatable from the database
        public IQueryable<Rental> GetRentals()
        {
            // Return the datatable
            return db.Rentals;
        }

        // GET: api/Rentals/5
        // Create a request to retrieve a data field from the database
        [ResponseType(typeof(Rental))]
        public IHttpActionResult GetRental(int id)
        {
            // Handle the find result with a variable
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return NotFound();
            }
            // Returnt the found data field
            return Ok(rental);
        }

        // PUT: api/Rentals/5
        // Create a request to update a datafield using the parameters(id, found 'rental' record)
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRental(int id, Rental rental)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rental.RentalID)
            {
                return BadRequest();
            }
            // Perform a modification to the database
            db.Entry(rental).State = EntityState.Modified;
            // try to save the changes
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            // Return a success status code
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Rentals
        // Create a request to add a datafield 
        [ResponseType(typeof(Rental))]
        public IHttpActionResult PostRental(Rental rental)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Add the datafield to the database
            db.Rentals.Add(rental);
            db.SaveChanges();

            // Return the route mapping of the added data field
            return CreatedAtRoute("DefaultApi", new { id = rental.RentalID }, rental);
        }

        // DELETE: api/Rentals/5
        // Create a request to delete a datafield according to the id
        [ResponseType(typeof(Rental))]
        public IHttpActionResult DeleteRental(int id)
        {
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return NotFound();
            }
            // Remove the datafield from the database
            db.Rentals.Remove(rental);
            // Save the changes to the database
            db.SaveChanges();
            // Return an OK resukt
            return Ok(rental);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RentalExists(int id)
        {
            return db.Rentals.Count(e => e.RentalID == id) > 0;
        }
    }
}