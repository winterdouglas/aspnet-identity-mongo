﻿namespace AspNet.Identity.MongoDB
{
    public class IdentityRoleClaim
	{
		public IdentityRoleClaim()
		{
		}

		public IdentityRoleClaim(string type, string value)
		{
			Type = type;
			Value = value;
		}

		public string Type { get; set; }
		public string Value { get; set; }
	}
}