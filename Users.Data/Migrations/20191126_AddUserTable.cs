using FluentMigrator;

namespace Users.Data.Migrations
{
    [Migration(20191126190100)]
    public class AddUserTable : Migration
    {
        public override void Down()
        {
            Delete.Table("User");
        }

        public override void Up()
        {
            Create.Table("User")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString()
                .WithColumn("Email").AsString()
                .WithColumn("Password").AsString()
                .WithColumn("Guid").AsString();
        }
    }
}
