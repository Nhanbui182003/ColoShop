﻿namespace WeBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Statistics", newName: "tb_Statistic");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.tb_Statistic", newName: "Statistics");
        }
    }
}
