using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using NutritionApp.Entities;


namespace NutritionApp.Web.DataContexts
{
    public class FoodsDb : DbContext
    {
        public FoodsDb()
            : base("DefaultConnection")
        {

        }

        public DbSet<Food> Food { get; set; }
    }
}