namespace WeBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class order : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_Order", "Code", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_Order", "Code", c => c.String(nullable: false));
        }
    }
}
