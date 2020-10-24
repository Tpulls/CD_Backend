//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CD_Backend.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Rental
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rental()
        {
            this.RentalItems = new HashSet<RentalItem>();
        }
    
        public int RentalID { get; set; }
        public int StaffID { get; set; }
        public System.DateTime DateRented { get; set; }
        public Nullable<System.DateTime> DateReturned { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RentalItem> RentalItems { get; set; }
    }
}