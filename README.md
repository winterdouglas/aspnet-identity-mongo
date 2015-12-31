AspNet.Identity.Mongo
=====================

A mongodb provider for the ASP.NET Identity framework 3.

## Usage

**WARNING** - This project is not fully functional yet. Integration tests are being updated to use xUnit. Not all functionality is guaranteed to work for now. Use at your own risk.

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
