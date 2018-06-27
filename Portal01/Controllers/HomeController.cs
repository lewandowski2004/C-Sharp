using ForumIT.Models;
using ForumIT.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForumIT.Controllers
{
    public class HomeController : Controller
    {
        private ForumContext db = new ForumContext();

       

        public ActionResult Index(int? id = 0, int? rozmiar = 5, int? strona = 1)
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
                                          IdKategorii = t.IdKategorii
                                      };
            
            if (id != 0)
            {
                lista = lista.Where(l => l.IdKategorii == id);
            }

            var model = new Multipledate()
            {
                listaa = lista.ToPagedList((int)strona, (int)rozmiar)
            };
            model.kategoriaa = db.Kategorie.ToList();
            return View(model);
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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
                                          IdKategorii = t.IdKategorii
                                      };
            if (id != 0)
            {
                lista = lista.Where(l => l.IdKategorii == id);
            }
            return View(lista.ToPagedList((int)strona, (int)rozmiar));
        }
    }
}