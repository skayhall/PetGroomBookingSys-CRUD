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
using PetGrooming.Models.ViewModels;
using System.Diagnostics;

namespace PetGrooming.Controllers
{
    public class PetController : Controller
    {

        private PetGroomingContext db = new PetGroomingContext();

        // GET: Pet
        public ActionResult List()
        {
            
            var pets = db.Pets.SqlQuery("Select * from Pets").ToList();
            return View(pets);
        }

        // GET: Pet/Details/5
        public ActionResult Show(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Pet pet = db.Pets.SqlQuery("select * from pets where petid=@PetID", new SqlParameter("@PetID",id)).FirstOrDefault();
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }

        //[HttpPost] Means that this method will only be activated on a POST form submit to the URL
        //URL: /Pet/Add
        [HttpPost] 
        public ActionResult Add(string PetName, Double PetWeight, String PetColor, int SpeciesID, string PetNotes)
        {
         
            ;

            //STEP 1: PULL DATA! The data is access as arguments to the method. Make sure the datatype is correct!
            //The variable name  MUST match the name attribute described in Views/Pet/Add.cshtml

            //STEP 2: FORMAT QUERY! the query will look something like "insert into () values ()"...
            string query = "insert into pets (PetName, Weight, color, SpeciesID, Notes) values (@PetName,@PetWeight,@PetColor,@SpeciesID,@PetNotes)";
            SqlParameter[] sqlparams = new SqlParameter[5]; // pieces of information to add
            // key and value pairs
            sqlparams[0] = new SqlParameter("@PetName",PetName);
            sqlparams[1] = new SqlParameter("@PetWeight", PetWeight);
            sqlparams[2] = new SqlParameter("@PetColor", PetColor);
            sqlparams[3] = new SqlParameter("@SpeciesID", SpeciesID);
            sqlparams[4] = new SqlParameter("@PetNotes",PetNotes);

            //db.Database.ExecuteSqlCommand will run insert, update, delete statements
            //db.Pets.SqlCommand will run a select statement, for example.
            db.Database.ExecuteSqlCommand(query, sqlparams);

            
            //returns list of pets!
            return RedirectToAction("List");
        }


        public ActionResult Add()
        {

            List<Species> species = db.Species.SqlQuery("select * from Species").ToList();

            return View(species);
        }


        public ActionResult Update(int id)
        {

            //get information about a pet
            Pet selectedpet = db.Pets.SqlQuery("SELECT * FROM pets WHERE petid =@id", new SqlParameter
                ("@id", id)).FirstOrDefault();

            List<Species> species = db.Species.SqlQuery("SELECT FROM species").ToList();

            UpdatePet viewmodel = new UpdatePet();
            viewmodel.pet = selectedpet;
            viewmodel.species = species;

            return View(selectedpet)
;
        }


        [HttpPost]

        public ActionResult Update(string PetName, string PetColor, double PetWeight, 
            int PetID, string Notes, string SpeciesName)
        {

            Debug.WriteLine("I am trying to edit a pet's name to" + PetName + "and change the" +
                "weight to" + PetWeight.ToString());

            //logic for updating the per in the database goes here

            string query = "UPDATE pets set Name=@PetName WHERE petid = @id";
            SqlParameter[] sqlparams = new SqlParameter[6];
            sqlparams[0] = new SqlParameter("@PetName", PetName);
            sqlparams[1] = new SqlParameter("@PetWeight", PetWeight);
            sqlparams[2] = new SqlParameter("@PetColor", PetColor);
            sqlparams[3] = new SqlParameter("@Notes", Notes);
            sqlparams[4] = new SqlParameter("@id", PetID);
            sqlparams[5] = new SqlParameter("@SpeciesName", SpeciesName);

            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");


        }

        public ActionResult Delete(int id)
        {

            string query = "DELETE FROM pets WHERE petid=@id";
            SqlParameter sqlparam = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, sqlparam);

            return RedirectToAction("List");

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
