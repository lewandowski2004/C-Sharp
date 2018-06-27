using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForumIT.Models
{
    public class Ikona
    {
        [Key]  //zbędne, gdyz w nazwie występuje Id
        public int IdIkony { get; set; }

        [Required]
        [Display(Name = "Nazwa ikony:")]
        public string NazwaIkony { get; set; }

    }
}