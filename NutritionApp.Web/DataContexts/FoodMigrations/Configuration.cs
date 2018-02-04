namespace NutritionApp.Web.DataContexts.FoodMigrations
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using NutritionApp.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Net;


    internal sealed class Configuration : DbMigrationsConfiguration<NutritionApp.Web.DataContexts.FoodsDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DataContexts\FoodMigrations";
        }



        protected override void Seed(NutritionApp.Web.DataContexts.FoodsDb context)
        {
            
           
            
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
