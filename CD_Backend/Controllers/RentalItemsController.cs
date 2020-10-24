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
    public class RentalItemsController : ApiController
    {
        // Instantiate the database
        private VR_CDEntities db = new VR_CDEntities();

        // GET: api/RentalItems
        // Retrieve the datatable 
        public IQueryable<RentalItem> GetRentalItems()
        {
            // Returnt the datatable
            return db.RentalItems;
        }

        // GET: api/RentalItems/5
        // Create a request to retrieve a datafield from the database using the input
        [ResponseType(typeof(RentalItem))]
        public IHttpActionResult GetRentalItem(int id)
        {
            // Create a variable to handle the find 
            RentalItem rentalItem = db.RentalItems.Find(id);
            if (rentalItem == null)
            {
                return NotFound();
            }
            // Return the data field
            return Ok(rentalItem);
        }

        [HttpGet]
        [Route("api/RentalItemsById/{id}")]
        [ResponseType(typeof(RentalItem))]
        public IQueryable<RentalItem> GetRentalItemsById(int id)
        {
            // to get the rental items of a particual rental id
            return db.RentalItems.Where(r => r.RentalID == id);
        }



        // PUT: api/RentalItems/5
        // Create a request to update a datafield 
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRentalItem(int id, RentalItem rentalItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rentalItem.RentalItemID)
            {
                return BadRequest();
            }
            // Modify the datafield
            db.Entry(rentalItem).State = EntityState.Modified;
            // Try to save the changes
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            // Return a successfull status code
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/RentalItems
        // Create a reques to add a datafield
        [ResponseType(typeof(RentalItem))]
        public IHttpActionResult PostRentalItem(RentalItem rentalItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Add the datafield to the database
            db.RentalItems.Add(rentalItem);
            // Try to save the changes
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (RentalItemExists(rentalItem.RentalItemID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            // returnt the route mapping of the added datafield
            return CreatedAtRoute("DefaultApi", new { id = rentalItem.RentalItemID }, rentalItem);
        }

        // DELETE: api/RentalItems/5
        // Create a request to delete a data field
        [ResponseType(typeof(RentalItem))]
        public IHttpActionResult DeleteRentalItem(int id)
        {
            // Create a variable to handle the find input
            RentalItem rentalItem = db.RentalItems.Find(id);
            if (rentalItem == null)
            {
                return NotFound();
            }
            // Remove the datafield
            db.RentalItems.Remove(rentalItem);
            // Save the data tabke
            db.SaveChanges();
            // Return an ok status code
            return Ok(rentalItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RentalItemExists(int id)
        {
            return db.RentalItems.Count(e => e.RentalItemID == id) > 0;
        }
    }
}