﻿using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Memoirs.Common.EntityFramework.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Memoirs.Common.EntityFramework
{
	public class ApplicationUser : IdentityUser {
		public async Task<ClaimsIdentity> GenerateUserIdentityAsync ( UserManager<ApplicationUser> manager ) {
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync ( this , DefaultAuthenticationTypes.ApplicationCookie );
			// Add custom user claims here
			return userIdentity;
		}

	    public virtual ICollection<Record> Records { get; set; }
	    public virtual ICollection<Entities.EndOfPeriod> EndOfPeriods { get; set; }
	}
}
