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
    public class PodKomentarzController : Controller
    {
        private ForumContext db = new ForumContext();

        // GET: PodKomentarz
        public ActionResult Index()
        {
            var podKomentarze = db.PodKomentarze.Include(p => p.Komentarz).Include(p => p.Uzytkownik);
            return View(podKomentarze.ToList());
        }

        // GET: PodKomentarz/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PodKomentarz podKomentarz = db.PodKomentarze.Find(id);
            if (podKomentarz == null)
            {
                return HttpNotFound();
            }
            return View(podKomentarz);
        }

       

        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Komentarz komentarz = db.Komentarze.Find(id);
           
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
        public ActionResult Create([Bind(Include = "IdPodKomentarza,Tresc,IdKomentarza")] PodKomentarz podKomentarz)
        {
            Komentarz komentarz = new Komentarz();

        if (ModelState.IsValid)
            {
                podKomentarz.DataDodania = System.DateTime.Now;
                podKomentarz.Id = User.Identity.GetUserId();
                db.PodKomentarze.Add(podKomentarz);
                db.SaveChanges();

                return RedirectToAction("Details", "Temat", new { id = podKomentarz.IdKomentarza});
            }

            ViewBag.IdKomentarza = new SelectList(db.Komentarze, "IdKomentarza", "Tresc", podKomentarz.IdKomentarza);
            ViewBag.Id = new SelectList(db.Users, "Id", "Email", podKomentarz.Id);
            return View(podKomentarz);
        }

        // GET: PodKomentarz/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PodKomentarz podKomentarz = db.PodKomentarze.Find(id);
            if (podKomentarz == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdKomentarza = new SelectList(db.Komentarze, "IdKomentarza", "Tresc", podKomentarz.IdKomentarza);
            ViewBag.Id = new SelectList(db.Users, "Id", "Email", podKomentarz.Id);
            return View(podKomentarz);
        }

        // POST: PodKomentarz/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPodKomentarza,Tresc,DataDodania,Id,IdKomentarza")] PodKomentarz podKomentarz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(podKomentarz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdKomentarza = new SelectList(db.Komentarze, "IdKomentarza", "Tresc", podKomentarz.IdKomentarza);
            ViewBag.Id = new SelectList(db.Users, "Id", "Email", podKomentarz.Id);
            return View(podKomentarz);
        }

        // GET: PodKomentarz/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PodKomentarz podKomentarz = db.PodKomentarze.Find(id);
            if (podKomentarz == null)
            {
                return HttpNotFound();
            }
            return View(podKomentarz);
        }

        // POST: PodKomentarz/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PodKomentarz podKomentarz = db.PodKomentarze.Find(id);
            db.PodKomentarze.Remove(podKomentarz);
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
