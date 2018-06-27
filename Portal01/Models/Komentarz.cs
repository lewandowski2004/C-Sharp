using ForumIT.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForumIT.Models
{
    public class Komentarz
    {
        [Key]
        [Display(Name = "Identyfikator:")]
        public int IdKomentarza { get; set; }

        [Required]
        [Display(Name = "Treść komentarza:")]
        [DataType(DataType.MultilineText)]
        public string Tresc { get; set; }

        [Display(Name = "Data dodania:")]
        [DataType(DataType.Date, ErrorMessage = "Niepoprawny format daty")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public System.DateTime DataDodania { get; set; }

        [Display(Name = "Autor komentarza:")]
        public string Id { get; set; }
        public virtual Uzytkownik Uzytkownik { get; set; }

        [Display(Name = "Komentowany tekst:")]
        public int IdTematu { get; set; }
        public virtual Temat Temat { get; set; }


        //Właściwość nawigacyjna
        public virtual ICollection<PodKomentarz> PodKomentarze { get; set; }
    }
}