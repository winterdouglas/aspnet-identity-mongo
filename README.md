AspNet.Identity.Mongo
=====================

A mongodb provider for the new ASP.NET Identity framework.

## Usage

This provider is based on default identity provider for Entity Framework

	//Create a new class and inherit from IdentityContextBase or one of it's descendants
	public class AuthContext : IdentityContext
	{
		public AuthContext()
		    : base("mongodb://localhost", "mydb")
		{
		}
	}
	
	//Configure identity in Startup.cs then invoke AddMongoStores<YourContext>()
	services.AddIdentity<IdentityUser, IdentityRole>().AddMongoStores<AuthContext>();
