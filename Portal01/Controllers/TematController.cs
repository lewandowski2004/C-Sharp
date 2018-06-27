using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ForumIT.Models;
using ForumIT.ViewModels;
using PagedList;
using Microsoft.AspNet.Identity;

namespace ForumIT.Controllers
{
    public class TematController : Controller
    {
        private ForumContext db = new ForumContext();

        // GET: Temat
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var tematy = db.Tematy.Include(t => t.Kategoria).Include(t => t.Uzytkownik);
            return View(tematy.ToList());
        }

        // GET: /Lista
        public ActionResult Lista(int? id = 0, int? rozmiar = 5, int? strona = 1)
        {
            IQueryable<Lista> lista = from t in db.Tematy
                                      orderby t.DataDodania descending
                                      select new Lista()
                                      {
                                          IdTematu = t.IdTematu,
                                          Tytul = t.Tytul,
                                          Tresc = t.Tresc,
                                          Autor = t.Uzytkownik.Imie + " " + t.Uzytkownik.Nazwisko,
                                          Foto = t.Uzytkownik.Foto,
                                          DataDodania = t.DataDodania,
                                          IdKategorii = t.IdKategorii
                                      };
            if (id != 0)
            {
                lista = lista.Where(l => l.IdKategorii == id);
            }
            return View(lista.ToPagedList((int)strona, (int)rozmiar));
        }
       
        // GET: Temat/Details/5
        [Authorize(Roles = "Admin, User")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temat temat = db.Tematy.Find(id);
            Komentarz komentarz = db.Komentarze.Find(id);
            if (temat == null)
            {
                return HttpNotFound();
            }
            var model = new MultidateTematKomentarz()
            {
                temat = temat,
                komentarz1 = komentarz
                
       
            };
            model.komentarz = db.Komentarze.ToList();
            
            return View(model);

        }

        // GET: Temat/Create
        [Authorize(Roles = "Admin, User")]
        public ActionResult Create()
        {
            ViewBag.IdKategorii = new SelectList(db.Kategorie, "IdKategorii", "NazwaKategorii");
            ViewBag.Id = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Temat/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTematu,Tytul,Tresc,DataDodania,IdKategorii,Id")] Temat temat)
        {
            if (ModelState.IsValid)
            {
                temat.DataDodania = System.DateTime.Now;
                temat.Id = User.Identity.GetUserId();
                db.Tematy.Add(temat);
                db.SaveChanges();
                return RedirectToAction("../Home");
            }

            ViewBag.IdKategorii = new SelectList(db.Kategorie, "IdKategorii", "NazwaKategorii", temat.IdKategorii);
            ViewBag.Id = new SelectList(db.Users, "Id", "Email", temat.Id);
            return View(temat);
        }


        // GET: Temat/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temat temat = db.Tematy.Find(id);
            if (temat == null)
            {
                return HttpNotFound();
            }
            return View(temat);
        }

        // POST: Temat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Temat temat = db.Tematy.Find(id);
            db.Tematy.Remove(temat);
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
