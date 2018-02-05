using NutritionApp.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NutritionApp.Web.DataContexts
{
    public class NutrientsDb : DbContext
    {
        public NutrientsDb()
            : base("DefaultConnection")
        {

        }

        public DbSet<Nutrient> Nutrient { get; set; }
    }
}