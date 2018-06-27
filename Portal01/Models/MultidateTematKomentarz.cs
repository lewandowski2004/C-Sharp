using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumIT.Models
{
    public class MultidateTematKomentarz
    {
        public ForumIT.Models.Temat temat { get; set; }
        public IEnumerable<ForumIT.Models.Komentarz> komentarz { get; set; }
        public ForumIT.Models.Komentarz komentarz1 { get; set; }

    }
}