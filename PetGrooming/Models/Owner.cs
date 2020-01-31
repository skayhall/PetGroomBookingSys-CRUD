using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//Install  entity framework 6 on Tools > Manage Nuget Packages > Microsoft Entity Framework (ver 6.4)
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace PetGrooming.Models
{
    public class Owner
    {

        public int OwnerID { get; set; }
        public string OwnerFname { get; set; }
        public string OwnerLname { get; set; }
        public string OwnerAddress { get; set; }
        public string OwnerWorkPhone { get; set; }
        public string OwnerHomePhone { get; set; }

    }
}