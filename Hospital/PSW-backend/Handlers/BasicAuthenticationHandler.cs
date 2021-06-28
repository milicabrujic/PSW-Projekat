using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Text.Encodings.Web;
using System.Net.Http.Headers;
using System.Text;
using System.Security.Claims;
using PSW_backend.Models;

namespace PSW_backend.Handlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private ApplicationDbContext _dbContext;

        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
                                          ILoggerFactory logger,
                                          UrlEncoder encoder,
                                          ISystemClock clock, ApplicationDbContext context) : base(options, logger, encoder, clock) => _dbContext = context;

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return Task.FromResult(AuthenticateResult.Fail("Authorization header was not found!"));

            if (GiveUserAuthorizations() == null)
                return Task.FromResult(AuthenticateResult.Fail("Authorization failed!"));

            System.Diagnostics.Debug.WriteLine("Authorization successfull!");
            return Task.FromResult(AuthenticateResult.Success(GiveUserAuthorizations()));
        }

        private AuthenticationTicket GiveUserAuthorizations()
        {
            if (GetAuthenticatedUser() == null)
                return null;

            return GetUserTicket(GetAuthenticatedUser());
        }

        private User GetAuthenticatedUser()
        {
            return _dbContext.Users.Where(user => user.Username == GetUserCredentials()[0] && user.Password == GetUserCredentials()[1]).FirstOrDefault(); ;
        }

        private string[] GetUserCredentials()
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]).Parameter)).Split(":");
        }

        private AuthenticationTicket GetUserTicket(User user)
        {
            var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                });
            var principal = new ClaimsPrincipal(identity);

            return new AuthenticationTicket(principal, Scheme.Name);
        }
    }
}