using CD_Backend.Models;
using CD_Backend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Management;

namespace CD_Backend.Controllers
{
    public class ReportsController : ApiController
    {

        private VR_CDEntities db = new VR_CDEntities();

        [HttpGet]
        [Route("api/Reports/GetRentalCountReports")]
        public IEnumerable<RentalCountReport> GetRentalCountReports()
        {
            return db.RentalCountReports;
        }    

        [HttpGet]
        [Route("api/Reports/GetRentedCDsReport")]
        public IEnumerable<RentedCDsViewModel> GetRentedCDsReport()
        {
            string sql = "SELECT RentalItem.RentalID, Staff.FirstName, Staff.LastName, CD.Title, Rental.DateRented " +
                "FROM Staff INNER JOIN " +
                "Rental ON Staff.StaffID = Rental.StaffID INNER JOIN " +
                "RentalItem ON Rental.RentalID = RentalItem.RentalID INNER JOIN " +
                "CD ON RentalItem.cdID = CD.cdID " +
                "ORDER BY Rental.DateRented DESC";

            return db.Database.SqlQuery<RentedCDsViewModel>(sql).ToList();
        }

    }
}
