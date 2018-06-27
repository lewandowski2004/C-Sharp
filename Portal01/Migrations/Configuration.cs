namespace ForumIT.Migrations
{
    using ForumIT.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ForumIT.Models.ForumContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ForumIT.Models.ForumContext context)
        {
            SeedRoles(context);
            SeedUsers(context);
            SeedIkony(context);
            SeedKategorie(context);

        }

        private void SeedRoles(ForumContext context)
        {
            var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists("User"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "User";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }
        }

        private void SeedUsers(ForumContext context)
        {
            var store = new UserStore<Uzytkownik>(context);
            var manager = new UserManager<Uzytkownik>(store);

            if (!context.Users.Any(u => u.UserName == "admin@forum.pl"))
            {
                var user = new Uzytkownik { UserName = "admin@forum.pl", Email = "admin@forum.pl", Imie = "Piotr", Nazwisko = "Pisarski", Foto = "autor1.jpg" };
                var adminresult = manager.Create(user, "Mz8jpg5a!");
                if (adminresult.Succeeded)
                    manager.AddToRole(user.Id, "Admin");
            }
            if (!context.Users.Any(u => u.UserName == "user@forum.pl"))
            {
                var user = new Uzytkownik { UserName = "user@forum.pl", Email = "user@forum.pl", Imie = "Anna", Nazwisko = "Autorska", Foto = "autor2.jpg" };
                var adminresult = manager.Create(user, "Mz8jpg5a!");
                if (adminresult.Succeeded)
                    manager.AddToRole(user.Id, "User");
            }

        }
        private void SeedIkony(ForumContext context)
        {
            var kat = new List<Ikona>
            {
                new Ikona {IdIkony = 1, NazwaIkony = "glyphicon glyphicon-star-empty"},
                new Ikona {IdIkony = 2, NazwaIkony = "glyphicon glyphicon-remove"},
                new Ikona {IdIkony = 3, NazwaIkony = "glyphicon glyphicon-list-alt"},
                new Ikona {IdIkony = 4, NazwaIkony = "glyphicon glyphicon-tint"},
                new Ikona {IdIkony = 5, NazwaIkony = "glyphicon glyphicon-leaf"},
                new Ikona {IdIkony = 6, NazwaIkony = "glyphicon glyphicon-wrench"}

            };
            kat.ForEach(i => context.Ikony.AddOrUpdate(i));
            context.SaveChanges();
        }

        private void SeedKategorie(ForumContext context)
        {
            var kat = new List<Kategoria>
            {
                new Kategoria {IdKategorii = 1, NazwaKategorii = "Programowanie", OpisKategorii="Wszystkie jezyki programowania.", IdIkony = 1 },
                new Kategoria {IdKategorii = 2, NazwaKategorii = "Elektronika", OpisKategorii="Wiadomoœci dotycz¹ce obszary elektroniki.",IdIkony = 2}

            };
            kat.ForEach(k => context.Kategorie.AddOrUpdate(k));
            context.SaveChanges();
        }



    }
}