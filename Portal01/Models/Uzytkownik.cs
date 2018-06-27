using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace ForumIT.Models
{
    // You can add profile data for the user by adding more properties to your Uzytkownik class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class Uzytkownik : IdentityUser
    {

        [Display(Name = "Imię użytkownika:")]
        [MaxLength(20, ErrorMessage = "Imię użytkownika nie może być dłuższe niż 20 znaków")]
        public string Imie { get; set; }

        [Display(Name = "Nazwisko użytkownika:")]
        [MaxLength(50, ErrorMessage = "Nazwisko użytkownika nie może być dłuższe niż 50 znaków")]
        public string Nazwisko { get; set; }

        #region dodatkowe pole nie odwzorowywane w bazie danych
        [NotMapped]
        [Display(Name = "Pan/Pani:")]
        public string PelneNazwisko
        {
            get { return Imie + " " + Nazwisko; }
        }
        #endregion

        [Display(Name = "Zdjęcie użytkownika:")]
        [MaxLength(128)]
        public string Foto { get; set; }

        public virtual ICollection<Temat> Temat { get; set; }
        public virtual ICollection<Komentarz> Komentarz { get; set; }



        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Uzytkownik> manager)
        {
            // Element authenticationType musi pasować do elementu zdefiniowanego w elemencie CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Dodaj tutaj niestandardowe oświadczenia użytkownika
            return userIdentity;
        }
    }
}