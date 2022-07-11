using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoExample.DAL.Domains
{
    public class Playlist
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        public string UserName { get; set; }

        public List<string> Items { get; set; }

        public Playlist(string username, List<string> movieIds)
        {
            this.UserName = username;
            this.Items = movieIds;
        }
    }
}
