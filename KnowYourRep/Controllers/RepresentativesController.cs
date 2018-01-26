using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KnowYourRep.Models;

namespace KnowYourRep.Content
{
    public class RepresentativesController : Controller
    {
        private KnowYourRepEntities db = new KnowYourRepEntities();

        // GET: Representatives
        public ActionResult Index()
        {
            var representatives = db.Representatives.Include(r => r.Affiliation).Include(r => r.District);
            return View(representatives.ToList());
        }

        // GET: Representatives/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Representative representative = db.Representatives.Find(id);
            if (representative == null)
            {
                return HttpNotFound();
            }
            return View(representative);
        }

        // GET: Representatives/Create
        public ActionResult Create()
        {
            ViewBag.AffiliationID = new SelectList(db.Affiliations, "AffiliationID", "PartyName");
            ViewBag.DistrictID = new SelectList(db.Districts, "DistrictID", "Location");
            return View();
        }

        // POST: Representatives/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RepID,RepName,Gender,Office,Education,Experience,DistrictID,AffiliationID")] Representative representative)
        {
            if (ModelState.IsValid)
            {
                db.Representatives.Add(representative);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AffiliationID = new SelectList(db.Affiliations, "AffiliationID", "PartyName", representative.AffiliationID);
            ViewBag.DistrictID = new SelectList(db.Districts, "DistrictID", "Location", representative.DistrictID);
            return View(representative);
        }

        // GET: Representatives/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Representative representative = db.Representatives.Find(id);
            if (representative == null)
            {
                return HttpNotFound();
            }
            ViewBag.AffiliationID = new SelectList(db.Affiliations, "AffiliationID", "PartyName", representative.AffiliationID);
            ViewBag.DistrictID = new SelectList(db.Districts, "DistrictID", "Location", representative.DistrictID);
            return View(representative);
        }

        // POST: Representatives/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RepID,RepName,Gender,Office,Education,Experience,DistrictID,AffiliationID")] Representative representative)
        {
            if (ModelState.IsValid)
            {
                db.Entry(representative).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AffiliationID = new SelectList(db.Affiliations, "AffiliationID", "PartyName", representative.AffiliationID);
            ViewBag.DistrictID = new SelectList(db.Districts, "DistrictID", "Location", representative.DistrictID);
            return View(representative);
        }

        // GET: Representatives/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Representative representative = db.Representatives.Find(id);
            if (representative == null)
            {
                return HttpNotFound();
            }
            return View(representative);
        }

        // POST: Representatives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Representative representative = db.Representatives.Find(id);
            db.Representatives.Remove(representative);
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
