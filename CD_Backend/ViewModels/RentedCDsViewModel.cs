using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CD_Backend.ViewModels
{
    public class RentedCDsViewModel
    {
        public int RentalID { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateRented { get; set; }
    }
}