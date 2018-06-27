using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ForumIT.Models
{
    public class Kategoria
    {
     

        [Key]  //zbędne, gdyz w nazwie występuje Id
        public int IdKategorii { get; set; }

        [Required]
        [Display(Name ="Nazwa katagorii:")]
        [MaxLength(20,ErrorMessage ="Nazwa kategorii nie może być dłuższa niż 20 znaków")]
        public string NazwaKategorii { get; set; }

        [Required]
        [Display(Name ="Opis kategorii:")]
        [MaxLength(255, ErrorMessage ="Opis musi być krótszy niż 255 znaków")]
        public string OpisKategorii { get; set; }


        [Display(Name = "Kategoria Tematu:")]
        public int IdIkony{ get; set; }
        public virtual Ikona Ikona { get; set; }

        //właściwość nawigacyjna
        public virtual ICollection<Temat> Tematy { get; set; }


    }
}