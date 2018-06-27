using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ForumIT.Models;
using Microsoft.AspNet.Identity;

namespace Portal01.Controllers
{
    public class KomentarzController : Controller
    {
        private ForumContext db = new ForumContext();

        // GET: Komentarz
        public ActionResult Index()
        {
            var komentarze = db.Komentarze.Include(k => k.Temat).Include(k => k.Uzytkownik);
            return View(komentarze);
        }

        // GET: Komentarz/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Komentarz komentarz = db.Komentarze.Find(id);
            if (komentarz == null)
            {
                return HttpNotFound();
            }
            return View(komentarz);
        }

        // GET: Komentarz/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Temat temat = db.Tematy.Find(id);
            ViewBag.Tytul = temat.Tytul;
            ViewBag.Id = id;
            /*
            ViewBag.IdTekstu = new SelectList(db.Teksty, "IdTekstu", "Tytul");
            ViewBag.Id = new SelectList(db.Users, "Id", "Email");
            */
            return View();
        }

        // POST: Komentarz/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdKomentarza,Tresc,IdTematu")] Komentarz komentarz)
        {
            if (ModelState.IsValid)
            {
                komentarz.DataDodania = System.DateTime.Now;
                komentarz.Id = User.Identity.GetUserId();
                db.Komentarze.Add(komentarz);
                db.SaveChanges();
                return RedirectToAction("Details", "Temat", new { id = komentarz.IdTematu });
            }

            ViewBag.IdTekstu = new SelectList(db.Tematy, "IdTematu", "Tytul", komentarz.IdKomentarza);
            ViewBag.Id = new SelectList(db.Users, "Id", "Email", komentarz.Id);
            return View(komentarz);
        }

        // GET: Komentarz/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Komentarz komentarz = db.Komentarze.Find(id);
            if (komentarz == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdTematu = new SelectList(db.Tematy, "IdTematu", "Tytul", komentarz.IdTematu);
            ViewBag.Id = new SelectList(db.Users, "Id", "Email", komentarz.Id);
            return View(komentarz);
        }

        // POST: Komentarz/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdKomentarza,Tresc,DataDodania,Id,IdTematu")] Komentarz komentarz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(komentarz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdTematu = new SelectList(db.Tematy, "IdTematu", "Tytul", komentarz.IdTematu);
            ViewBag.Id = new SelectList(db.Users, "Id", "Email", komentarz.Id);
            return View(komentarz);
        }

        // GET: Komentarz/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Komentarz komentarz = db.Komentarze.Find(id);
            if (komentarz == null)
            {
                return HttpNotFound();
            }
            return View(komentarz);
        }

        // POST: Komentarz/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Komentarz komentarz = db.Komentarze.Find(id);
            db.Komentarze.Remove(komentarz);
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
