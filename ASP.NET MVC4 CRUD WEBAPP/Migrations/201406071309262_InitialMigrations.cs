namespace ASP.NET_MVC4_CRUD_WEBAPP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigrations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Auctions", "LastBindingPerson", c => c.String(maxLength: 400));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Auctions", "LastBindingPerson");
        }
    }
}
