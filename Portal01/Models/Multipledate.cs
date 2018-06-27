using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace ForumIT.Models
{
    public class Multipledate
    {
        public PagedList.IPagedList<ForumIT.ViewModels.Lista> listaa { get; set; }
        public IEnumerable<ForumIT.Models.Kategoria> kategoriaa { get; set; }
    }
}