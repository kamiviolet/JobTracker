using MongoDB.Bson.Serialization.Attributes;

namespace JobTracker.Models
{
    public class JobEntry
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Position { get; set; } = null!;

        public string? Requirement { get; set; }

        public string Company { get; set; } = null!;

        public string Location { get; set; } = null!;

        public string? Contact { get; set; }

        public string? Phone { get; set; }

        public string Email { get; set; } = null!;

        public string? Agency { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTime? DateApplied { get; set; }

        public string Status { get; set; } = "pending";
    }

    public enum Status { pending, tech_test, phone_interview, in_person_interview, rejected, accepted }
}
