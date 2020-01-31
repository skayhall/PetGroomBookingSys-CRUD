using System;
using System.Collections.Generic;
using System.Data;
//required for SqlParameter class
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PetGrooming.Data;
using PetGrooming.Models;
using System.Diagnostics;
namespace PetGrooming.Controllers
{
    public class SpeciesController : Controller
    {
        private PetGroomingContext db = new PetGroomingContext();
        // GET: Species
        public ActionResult Index()
        {
            return View();
        }

        //TODO: Each line should be a separate method in this class
        // List

        public ActionResult List()
        {

            List<Species> myspecies = db.Species.SqlQuery("SELECT * FROM species").ToList();

            //data we need - 
            return View(myspecies);

        }


        public ActionResult Add()
        {

            return View();

        }

        [HttpPost]


        public ActionResult Add(string SpeciesName)
        {

            //STEP 1 - GATHER INPUT DATA FOR PET SPECIES
            Debug.WriteLine("Gathering species name FROM" + SpeciesName); //debuggingline

            //STEP 2 - CREATE QUERY
            string query = "INSERT INTO species (Name) values (@SpeciesName)";
            SqlParameter sqlparam = new SqlParameter("@SpeciesName", SpeciesName);

            //STEP 3 - RUN QUERY
            db.Database.ExecuteSqlCommand(query, sqlparam);

            //STEP 4 - GO BACK TO LIST OF SPECIES
            return RedirectToAction("List");

        }


        public ActionResult Show(int id)
        {

            //information needed from the database to make species show to the end user
            string query = "SELECT * FROM species WHERE speciesid = @id";

            SqlParameter sqlparam = new SqlParameter("@id", id);

            Species selectedspecies = db.Species.SqlQuery(query,sqlparam).FirstOrDefault();

            return View(selectedspecies);

        }


        public ActionResult Update(int id)
        {

            string query = "SELECT * FROM species WHERE speciesid = @id";
            SqlParameter sqlparam = new SqlParameter("@id", id);

            Species selectedspecies = db.Species.SqlQuery(query, sqlparam).FirstOrDefault();

            return View(selectedspecies);

        }

        [HttpPost]
        public ActionResult Update(int id, string SpeciesName)
        {

            string query = "UPDATE species set Name=@SpeciesName WHERE speciesid = @id";
            SqlParameter[] sqlparams = new SqlParameter[2]; //two items are in here. name and ID
            sqlparams[0] = new SqlParameter("@SpeciesName", SpeciesName);
            sqlparams[1] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");

        }


        public ActionResult Delete(int id)
        {

            string query = "DELETE FROM species WHERE speciesid=@id";
            SqlParameter sqlparam = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, sqlparam);

            return RedirectToAction("List");

        }


        // Show
        // Add
        // [HttpPost] Add
        // Update
        // [HttpPost] Update
        // (optional) delete
        // [HttpPost] Delete
    }
}