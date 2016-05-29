using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Memoirs.Identity {
	// Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
	public sealed class ApplicationUserManager : UserManager<ApplicationUser> {
		public ApplicationUserManager ( IUserStore<ApplicationUser> store )
			: base ( store ) {
            UserValidator = new UserValidator<ApplicationUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            PasswordValidator = new PasswordValidator
            {
                //RequiredLength = 6 ,
                //RequireNonLetterOrDigit = false ,
                //RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            EmailService = new EmailService();
            SmsService = new SmsService();
            
            // from create method:
            //var dataProtectionProvider = options.DataProtectionProvider; 
            //if (dataProtectionProvider != null)
            //{
            //    UserTokenProvider =
            //        new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            //}
        }
	}
}
