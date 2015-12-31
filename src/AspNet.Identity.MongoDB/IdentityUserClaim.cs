﻿namespace AspNet.Identity.MongoDB
{
	using System.Security.Claims;

	public class IdentityUserClaim
	{
		public IdentityUserClaim()
		{
		}

		public IdentityUserClaim(string type, string value)
		{
			Type = type;
			Value = value;
		}

		public string Type { get; set; }
		public string Value { get; set; }
	}
}