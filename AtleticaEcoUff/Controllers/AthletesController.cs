using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AtleticaEcoUff.Models;

namespace AtleticaEcoUff.Controllers
{
    [Authorize]
    public class AthletesController : Controller
    {
        //disable the automatic db connn
        //private AthleticsModel db = new AthleticsModel();

        private IAthletesMock db;

        public AthletesController()
        {
            this.db = new EFAthletes();
        }


        // mock constructor
        public AthletesController(IAthletesMock mock)

        {
            this.db = mock;
        }


        [OverrideAuthorization]
        // GET: Athletes
        public ActionResult Index()
        {
            var sports = db.Athlete.Include(a => a.Sport);
            return View("Index", sports.ToList());
        }

        [OverrideAuthorization]
        // GET: Athletes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }

            //Athlete athlete = db.Athletes.Find(id);
            Athlete athlete = db.Athlete.SingleOrDefault(a => a.athlete_id == id);

            if (athlete == null)
            {
                //return HttpNotFound();
                return View("Error");

            }
            return View("Details", athlete);

        }

        // GET: Athletes/Create
        public ActionResult Create()
        {
            ViewBag.sport_id = new SelectList(db.Sports, "sport_id", "sport_name");
            return View("Create");
        }

        // POST: Athletes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "athlete_id,athlete_age, athlete_name,sport_id")] Athlete athlete)
        {
            if (ModelState.IsValid)
            {

                db.Save(athlete);
                return RedirectToAction("Index");
            }

            ViewBag.sport_id = new SelectList(db.Sports, "sport_id", "sport_name", athlete.sport_id);
            return View("Create", athlete);
        }

        // GET: Athletes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            Athlete athlete = db.Athlete.SingleOrDefault(a => a.athlete_id == id);
            if (athlete == null)
            {
                //return HttpNotFound();
                return View("Error");
            }
            ViewBag.sport_id = new SelectList(db.Sports, "sport_id", "sport_name", athlete.sport_id);
            return View("Edit", athlete);
        }

        // POST: Athletes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "athlete_id, athlete_age, athlete_name,sport_id")] Athlete athlete)
        {
            if (ModelState.IsValid)
            {

                db.Save(athlete);
                return RedirectToAction("Index");
            }
            ViewBag.sport_id = new SelectList(db.Sports, "sport_id", "sport_name", athlete.sport_id);
            return View("Edit", athlete);
        }

        // GET: Athletes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            //Athlete athlete = db.Athletes.Find(id);
            Athlete athlete = db.Athlete.SingleOrDefault(a => a.athlete_id == id);

            if (athlete == null)
            {
                //return HttpNotFound();
                return View("Error");
            }
            return View("Delete", athlete);
        }

        // POST: Athletes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            //Athlete athlete = db.Athletes.Find(id);
            Athlete athlete = db.Athlete.SingleOrDefault(a => a.athlete_id == id);

            // db.Athletes.Remove(athlete);
            // db.SaveChanges();
            db.Delete(athlete);
            if (id == null)
            {
                return View("Error");
            }
            if (athlete == null)
            {
                return View("Error");
            }

            else
            {
                return RedirectToAction("Index");
            }
            //protected override void Dispose(bool disposing)
            //{
            //    if (disposing)
            //    {
            //        db.Dispose();
            //    }
            //    base.Dispose(disposing);
            //}
        }
    }
}
