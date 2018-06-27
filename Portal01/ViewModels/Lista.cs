namespace ForumIT.ViewModels
{
    public class Lista
    {
        public int IdTematu { get; set; }
        public string Tytul { get; set; }
        public string Tresc { get; set; }
        public string Autor { get; set; }
        public string Foto { get; set; }
        public int IdKategorii { get; set; }
        public System.DateTime DataDodania { get; set; }

    }

    public class CommentsVM
    {
        public int IdKomentarza { get; set; }
        public string Tresc { get; set; }
        public System.DateTime DataDodania { get; set; }
        public int IdTematu { get; set; }
        public string Autor { get; set; }
        public string Foto { get; set; }
    }


    public class SubCommentsVM
    {
        public int IdPodKomentarza { get; set; }
        public string Tresc { get; set; }
        public System.DateTime DataDodania { get; set; }
        public int IdKomentarza { get; set; }
        public string Autor { get; set; }
        public string Foto { get; set; }
    }
}