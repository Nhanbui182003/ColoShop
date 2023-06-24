namespace WeBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeLengthModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Product", "ProductCode", c => c.String(maxLength: 50));
            AlterColumn("dbo.tb_Post", "Alias", c => c.String(maxLength: 150));
            AlterColumn("dbo.tb_Post", "Image", c => c.String(maxLength: 150));
            AlterColumn("dbo.tb_Post", "SeoTitle", c => c.String(maxLength: 250));
            AlterColumn("dbo.tb_Product", "Alias", c => c.String(maxLength: 250));
            AlterColumn("dbo.tb_Product", "Image", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_Product", "Image", c => c.String());
            AlterColumn("dbo.tb_Product", "Alias", c => c.String());
            AlterColumn("dbo.tb_Post", "SeoTitle", c => c.String());
            AlterColumn("dbo.tb_Post", "Image", c => c.String());
            AlterColumn("dbo.tb_Post", "Alias", c => c.String());
            DropColumn("dbo.tb_Product", "ProductCode");
        }
    }
}
