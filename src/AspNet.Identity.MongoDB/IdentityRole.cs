using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace AspNet.Identity.MongoDB
{
    public class IdentityRole
    {
        public IdentityRole()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

        public IdentityRole(string roleName)
            : this()
        {
            Name = roleName;
        }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }

        public string Name { get; set; }

        public virtual string NormalizedName { get; set; }

        [BsonIgnoreIfNull]
        public virtual ICollection<IdentityRoleClaim> Claims { get; set; } = new List<IdentityRoleClaim>();

        //public virtual void AddClaim(Claim claim)
        //{
        //    Claims.Add(new IdentityRoleClaim(claim.Type, claim.Value));
        //}

        //public virtual void RemoveClaim(Claim claim)
        //{
        //    var claimsToRemove = Claims
        //        .Where(c => c.Type == claim.Type)
        //        .Where(c => c.Value == claim.Value);

        //    Claims = Claims.Except(claimsToRemove).ToList();
        //}

        public override string ToString()
        {
            return Name;
        }
    }
}