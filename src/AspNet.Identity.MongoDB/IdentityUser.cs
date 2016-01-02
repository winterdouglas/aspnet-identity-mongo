using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace AspNet.Identity.MongoDB
{
    public class IdentityUser
    {
        public IdentityUser()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

        public IdentityUser(string userName)
            : this()
        {
            UserName = userName;
        }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }

        public string UserName { get; set; }

        public virtual string NormalizedUserName { get; set; }

        public virtual string Email { get; set; }

        public virtual string NormalizedEmail { get; set; }

        public virtual bool EmailConfirmed { get; set; }

        [BsonIgnoreIfNull]
        public virtual string PasswordHash { get; set; }

        public virtual string SecurityStamp { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual bool PhoneNumberConfirmed { get; set; }

        public virtual bool TwoFactorEnabled { get; set; }

        public virtual DateTime? LockoutEndDateUtc { get; set; }

        public virtual bool LockoutEnabled { get; set; }

        public virtual int AccessFailedCount { get; set; }

        [BsonIgnoreIfNull]
        public virtual ICollection<string> Roles { get; set; } = new List<string>();

        [BsonIgnoreIfNull]
        public virtual ICollection<IdentityUserClaim> Claims { get; set; } = new List<IdentityUserClaim>();

        [BsonIgnoreIfNull]
        public virtual ICollection<IdentityUserLogin> Logins { get; set; } = new List<IdentityUserLogin>();

        //public virtual void AddRole(string role)
        //{
        //    Roles.Add(role);
        //}

        //public virtual void RemoveRole(string role)
        //{
        //    Roles.Remove(role);
        //}

        //public virtual void AddClaim(Claim claim)
        //{
        //    Claims.Add(new IdentityUserClaim(claim.Type, claim.Value));
        //}

        //public virtual void RemoveClaim(Claim claim)
        //{
        //    var claimsToRemove = Claims
        //        .Where(c => c.Type == claim.Type)
        //        .Where(c => c.Value == claim.Value);

        //    Claims = Claims.Except(claimsToRemove).ToList();
        //}

        //public virtual void ReplaceClaim(Claim claim, Claim newClaim)
        //{
        //    var matchedClaims = Claims.Where(uc => uc.Value == claim.Value && uc.Type == claim.Type);
        //    foreach (var matchedClaim in matchedClaims)
        //    {
        //        matchedClaim.Value = newClaim.Value;
        //        matchedClaim.Type = newClaim.Type;
        //    }
        //}

        //public virtual void AddLogin(IdentityUserLogin login)
        //{
        //    Logins.Add(login);
        //}

        //public virtual void RemoveLogin(IdentityUserLogin login)
        //{
        //    var loginsToRemove = Logins
        //        .Where(l => l.LoginProvider == login.LoginProvider)
        //        .Where(l => l.ProviderKey == login.ProviderKey);

        //    Logins = Logins.Except(loginsToRemove).ToList();
        //}

        public override string ToString()
        {
            return UserName;
        }
    }
}