namespace WeBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uodateSystemSetting : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.tb_SystemSetting");
            AlterColumn("dbo.tb_SystemSetting", "SettingKey", c => c.String(nullable: false, maxLength: 50));
            AddPrimaryKey("dbo.tb_SystemSetting", "SettingKey");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.tb_SystemSetting");
            AlterColumn("dbo.tb_SystemSetting", "SettingKey", c => c.String(nullable: false, maxLength: 50));
            AddPrimaryKey("dbo.tb_SystemSetting", "SettingKey");
        }
    }
}
