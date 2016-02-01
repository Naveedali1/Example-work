using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Scout7.Models;

namespace Scout7.Controllers
{
    public class PlayersController : Controller
    {
        private PlayersDBContext db = new PlayersDBContext();
        //the authorize tag specifies that only users you meet the authorization requirment can access this controller
        [Authorize]
      
        //the main scout 7 index/homepage, also includes the search function
        public ActionResult Index(string searchName, string searchDob, string searchClub  )
        {
            var players = from p in db.Players
                          select p;

            if (!string.IsNullOrWhiteSpace(searchName))
            {
                if (searchName.Trim().Contains(' '))
                {
                    var names = searchName.Trim().Split(' ');
                    var firstName = names[0].ToString();
                    string surname = string.Empty;
                    for (int i = 1; i < names.Length; i++ )
                    {
                        surname = surname + " " + names[i].ToString();
                    }
                    surname = surname.Trim();
                    players = players.Where(s => s.FirstName.Contains(firstName) && s.Surname.Contains(surname));
                }
                else 
                {
                    players = players.Where(s => s.FirstName.Contains(searchName) || s.Surname.Contains(searchName));
                }
            }

            if (!String.IsNullOrEmpty(searchDob))
            {
                var dob = Convert.ToDateTime(searchDob);
                players = players.Where(s => s.DateOfBirth == dob);
            }

            if (!String.IsNullOrEmpty(searchClub))
            {
                players = players.Where(s => s.CurrentClub.Contains(searchClub));
            }
            
            return View(players);
            
        }

        // the details view which retrieves a specific players details and displays them
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlayersModel playersModel = db.Players.Find(id);
            if (playersModel == null)
            {
                return HttpNotFound();
            }
            return View(playersModel);
        }

        // this returns the create player view 
        public ActionResult Create()
        {
            return View();
        }

        // allows the user to create a player by entering the required data 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,Surname,DateOfBirth,CurrentClub")] PlayersModel playersModel)
        {
            if (ModelState.IsValid)
            {
                db.Players.Add(playersModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(playersModel);
        }

        // returns the player edit page which allows the user to make any ammendments to a players record
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlayersModel playersModel = db.Players.Find(id);
            if (playersModel == null)
            {
                return HttpNotFound();
            }
            return View(playersModel);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,Surname,DateOfBirth,CurrentClub")] PlayersModel playersModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(playersModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(playersModel);
        }

        // allows the user to delete a players record
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlayersModel playersModel = db.Players.Find(id);
            if (playersModel == null)
            {
                return HttpNotFound();
            }
            return View(playersModel);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlayersModel playersModel = db.Players.Find(id);
            db.Players.Remove(playersModel);
            db.SaveChanges();
            return RedirectToAction("Index");
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
