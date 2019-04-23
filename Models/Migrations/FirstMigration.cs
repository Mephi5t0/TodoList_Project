using Models.Users;
using Mongo.Migration.Migrations;
using MongoDB.Bson;

namespace Models.Migrations
{
    public class FirstMigration : Migration<User>
    {
        public FirstMigration()
            : base("0.0.1")
        {
        }

        public override void Up(BsonDocument document)
        {
            var doors = document["PasswordHash"].ToInt32();
            document.Add("Password", doors);
            document.Remove("PasswordHash");
        }

        public override void Down(BsonDocument document)
        {
            var doors = document["Password"].ToInt32();
            document.Add("PasswordHash", doors);
            document.Remove("Password");
        }
    }
}