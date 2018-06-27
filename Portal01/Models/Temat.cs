using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ForumIT.Models
{
    public class Temat
    {

        [Key]
        [Display(Name = "Identyfikator:")]
        public int IdTematu { get; set; }

        [Required]
        [Display(Name = "Tytuł tematu:")]
        [MaxLength(75, ErrorMessage = "Tytuł tekstu nie może przekraczać 75 znaków")]
        public string Tytul { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "Treść tematu:")]
        public string Tresc { get; set; }


        [Required]
        [Display(Name = "Data dodania:")]
        [DataType(DataType.Date, ErrorMessage = "Niepoprawny format daty")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime DataDodania { get; set; }

        [Display(Name = "Kategoria Tematu:")]
        public int IdKategorii { get; set; }
        public virtual Kategoria Kategoria { get; set; }

        [Display(Name = "Autor Tematu:")]
        public string Id { get; set; }
        public virtual Uzytkownik Uzytkownik { get; set; }

        //Właściwość nawigacyjna
        public virtual ICollection<Komentarz> Komentarze { get; set; }


    }
}