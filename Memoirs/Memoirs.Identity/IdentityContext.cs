using Memoirs.EntityFramework;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Memoirs.Identity {
	public class IdentityContext : IdentityDbContext<ApplicationUser> {
	    public IdentityContext(string connectionName)
            :base(connectionName)
	    {
	    }

	    public IdentityContext ()
			: base ( "IdentityConnection" , throwIfV1Schema: false ) {
		}

		public static IdentityContext Create () {
			return new IdentityContext ();
		}
	}
}
