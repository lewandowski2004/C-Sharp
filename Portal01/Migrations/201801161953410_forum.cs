namespace ForumIT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class forum : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ikona",
                c => new
                    {
                        IdIkony = c.Int(nullable: false, identity: true),
                        NazwaIkony = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IdIkony);
            
            CreateTable(
                "dbo.Kategoria",
                c => new
                    {
                        IdKategorii = c.Int(nullable: false, identity: true),
                        NazwaKategorii = c.String(nullable: false, maxLength: 20),
                        OpisKategorii = c.String(nullable: false, maxLength: 255),
                        IdIkony = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdKategorii)
                .ForeignKey("dbo.Ikona", t => t.IdIkony)
                .Index(t => t.IdIkony);
            
            CreateTable(
                "dbo.Temat",
                c => new
                    {
                        IdTematu = c.Int(nullable: false, identity: true),
                        Tytul = c.String(nullable: false, maxLength: 75),
                        Tresc = c.String(nullable: false),
                        DataDodania = c.DateTime(nullable: false),
                        IdKategorii = c.Int(nullable: false),
                        Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IdTematu)
                .ForeignKey("dbo.Kategoria", t => t.IdKategorii)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.IdKategorii)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Komentarz",
                c => new
                    {
                        IdKomentarza = c.Int(nullable: false, identity: true),
                        Tresc = c.String(nullable: false),
                        DataDodania = c.DateTime(nullable: false),
                        Id = c.String(nullable: false, maxLength: 128),
                        IdTematu = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdKomentarza)
                .ForeignKey("dbo.Temat", t => t.IdTematu)
                .ForeignKey("dbo.AspNetUsers", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.IdTematu);
            
            CreateTable(
                "dbo.PodKomentarz",
                c => new
                    {
                        IdPodKomentarza = c.Int(nullable: false, identity: true),
                        Tresc = c.String(nullable: false),
                        DataDodania = c.DateTime(nullable: false),
                        Id = c.String(maxLength: 128),
                        IdKomentarza = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdPodKomentarza)
                .ForeignKey("dbo.Komentarz", t => t.IdKomentarza)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.IdKomentarza);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Imie = c.String(maxLength: 20),
                        Nazwisko = c.String(maxLength: 50),
                        Foto = c.String(maxLength: 128),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Komentarz", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Komentarz", "IdTematu", "dbo.Temat");
            DropForeignKey("dbo.PodKomentarz", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Temat", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PodKomentarz", "IdKomentarza", "dbo.Komentarz");
            DropForeignKey("dbo.Temat", "IdKategorii", "dbo.Kategoria");
            DropForeignKey("dbo.Kategoria", "IdIkony", "dbo.Ikona");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.PodKomentarz", new[] { "IdKomentarza" });
            DropIndex("dbo.PodKomentarz", new[] { "Id" });
            DropIndex("dbo.Komentarz", new[] { "IdTematu" });
            DropIndex("dbo.Komentarz", new[] { "Id" });
            DropIndex("dbo.Temat", new[] { "Id" });
            DropIndex("dbo.Temat", new[] { "IdKategorii" });
            DropIndex("dbo.Kategoria", new[] { "IdIkony" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.PodKomentarz");
            DropTable("dbo.Komentarz");
            DropTable("dbo.Temat");
            DropTable("dbo.Kategoria");
            DropTable("dbo.Ikona");
        }
    }
}
