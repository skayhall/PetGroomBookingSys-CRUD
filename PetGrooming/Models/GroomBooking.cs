using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//Install  entity framework 6 on Tools > Manage Nuget Packages > Microsoft Entity Framework (ver 6.4)
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace PetGrooming.Models
{
    public class GroomBooking
    {
 
        [Key]
        public int GroomBookingID { get; set; }
        public DateTime GroomBookingDate { get; set; }
        //established as the price of the whole appointment (13% HST tax included)
        //price is in cents i.e. (12664cents) = $126.64
        //currency is CANADIAN (cad)
        public int GroomBookingPrice { get; set; }
    }
}