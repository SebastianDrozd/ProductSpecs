using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ProductSpecs.Exceptions;
using System.DirectoryServices.AccountManagement;

namespace ProductSpecs.Services
{
    public class LdapService
    {
        private readonly IConfiguration _config;

        public LdapService(IConfiguration config)
        {
            _config = config;
        }

        public bool ValidateUser(string username, string password) 
        {
            try
            {
                using var context = new PrincipalContext(
              ContextType.Domain,
              "BOBAK.LOCAL"
              );
                return context.ValidateCredentials(username, password);
            }
            catch (PrincipalServerDownException ex) 
            {
                throw new ServiceUnavailableException("Active Directly is unavailable");
            }
          
           
        }
    }
}
