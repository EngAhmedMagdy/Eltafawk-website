using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace EltafawkPlatform.Models
{
    public class StudentModel
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string WhatsappNumber { get; set; }
        public string ProfileImage { get; set; }

    }

}

