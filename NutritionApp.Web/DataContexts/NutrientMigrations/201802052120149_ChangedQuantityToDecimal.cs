namespace NutritionApp.Web.DataContexts.NutrientMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedQuantityToDecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Nutrients", "Quantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Nutrients", "Quantity", c => c.Int(nullable: false));
        }
    }
}
