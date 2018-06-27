using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ForumIT.Models;

namespace ForumIT.Controllers
{
    public class KategoriaController : Controller
    {
        private ForumContext db = new ForumContext();

        // GET: Kategorias
        [Authorize(Roles = "Admin, User")]
        public ActionResult Index()
        {
            var kategorie = db.Kategorie.Include(k => k.Ikona);
            return View(kategorie.ToList());
        }

        // GET: Kategorias/Details/5
        [Authorize(Roles = "Admin, User")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategoria kategoria = db.Kategorie.Find(id);
            if (kategoria == null)
            {
                return HttpNotFound();
            }
            return View(kategoria);
        }

        // GET: Kategorias/Create
        [Authorize(Roles = "Admin, User")]
        public ActionResult Create()
        {
            ViewBag.IdIkony = new SelectList(db.Ikony, "IdIkony", "NazwaIkony");
            return View();
        }

        // POST: Kategorias/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdKategorii,NazwaKategorii,OpisKategorii,IdIkony")] Kategoria kategoria)
        {
            if (ModelState.IsValid)
            {
                db.Kategorie.Add(kategoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdIkony = new SelectList(db.Ikony, "IdIkony", "NazwaIkony", kategoria.IdIkony);
            return View(kategoria);
        }

        // GET: Kategorias/Edit/5
        [Authorize(Roles = "Admin, User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategoria kategoria = db.Kategorie.Find(id);
            if (kategoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdIkony = new SelectList(db.Ikony, "IdIkony", "NazwaIkony", kategoria.IdIkony);
            return View(kategoria);
        }

        // POST: Kategorias/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdKategorii,NazwaKategorii,OpisKategorii,IdIkony")] Kategoria kategoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kategoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdIkony = new SelectList(db.Ikony, "IdIkony", "NazwaIkony", kategoria.IdIkony);
            return View(kategoria);
        }

        // GET: Kategorias/Delete/5
        [Authorize(Roles = "Admin, User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategoria kategoria = db.Kategorie.Find(id);
            if (kategoria == null)
            {
                return HttpNotFound();
            }
            return View(kategoria);
        }

        // POST: Kategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kategoria kategoria = db.Kategorie.Find(id);
            db.Kategorie.Remove(kategoria);
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
