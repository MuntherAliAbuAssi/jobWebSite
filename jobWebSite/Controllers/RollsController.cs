using jobWebSite.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jobWebSite.Controllers
{
    [Authorize(Roles ="Admins")]

    public class RollsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Rolls
        public ActionResult Index()
        {
            return View(db.Roles.ToList());
        }

        // GET: Rolls/Details/5
        public ActionResult Details(string id)
        {
            var role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: Rolls/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rolls/Create
        [HttpPost]
        public ActionResult Create(IdentityRole role)
        {

            if (ModelState.IsValid)
            {
                db.Roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);


        }

        // GET: Rolls/Edit/5
        public ActionResult Edit(string id)
        {
            var role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Rolls/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Name")] IdentityRole role)
        {

            if (ModelState.IsValid)
            {
                db.Entry(role).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(role);

        }


        // GET: Rolls/Delete/5
        [HttpGet]
        public ActionResult Delete(string id)
        {
            var roleDelId = db.Roles.Find(id);
            if (roleDelId == null)
            {
                return HttpNotFound();
            }
            return View(roleDelId);
        }

        // POST: Rolls/Delete/5
        [HttpPost]
        public ActionResult Delete(IdentityRole role)
        {
            
                if (ModelState.IsValid)
                {
                  var roldel=  db.Roles.Find(role.Id);
                    db.Roles.Remove(roldel);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                return View(role);
        }

    } 
}

