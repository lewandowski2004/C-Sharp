using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ForumIT.Models
{


    public class ForumContext : IdentityDbContext
    {
        public ForumContext() : base("Forum")
        {
        }

        public static ForumContext Create()
        {
            return new ForumContext();
        }

        public DbSet<Kategoria> Kategorie { get; set; }
        public DbSet<Temat> Tematy { get; set; }
        public DbSet<PodKomentarz> PodKomentarze { get; set; }
        public DbSet<Komentarz> Komentarze { get; set; }
        public DbSet<Ikona> Ikony { get; set; }
        public DbSet<Uzytkownik> Uzytkownicy { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<Komentarz>().HasRequired(x => x.Uzytkownik).WithMany(x => x.Komentarz)
                .HasForeignKey(x => x.Id).WillCascadeOnDelete(true);

        }

    }
}