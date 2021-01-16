namespace FinalniTest9.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using FinalniTest9.Models;
    internal sealed class Configuration : DbMigrationsConfiguration<FinalniTest9.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FinalniTest9.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Lanacs.AddOrUpdate(x => x.Id,

            new Lanac() { Id = 1, Naziv = "Hilton Worldwide", GodinaOsnivanja = 1919},
            new Lanac() { Id = 2, Naziv = "Marriott International", GodinaOsnivanja = 1927 },
            new Lanac() { Id = 3, Naziv = "Kempinski", GodinaOsnivanja = 1897 }

           );

            context.Hotels.AddOrUpdate(x => x.Id,


           new Hotel() { Id = 1, Naziv ="Sheraton Novi Sad", GodinaOtvararanja =  2018, BrojZaposlenih = 70, BrojSoba = 150, LanacId = 2 },
           new Hotel() { Id = 2, Naziv = "Hilton Belgrade", GodinaOtvararanja = 2017, BrojZaposlenih = 100, BrojSoba = 242, LanacId = 1 },
           new Hotel() { Id = 3, Naziv = "Palais Hansen", GodinaOtvararanja = 2013, BrojZaposlenih = 80, BrojSoba = 152, LanacId = 3 },
           new Hotel() { Id = 4, Naziv = "Budapest Marriott", GodinaOtvararanja = 1994, BrojZaposlenih = 130, BrojSoba = 364, LanacId = 2 },
           new Hotel() { Id = 5, Naziv = "Hilton Berlin", GodinaOtvararanja = 1991, BrojZaposlenih = 200, BrojSoba = 601, LanacId = 1 }

           );
        }
    }
}
