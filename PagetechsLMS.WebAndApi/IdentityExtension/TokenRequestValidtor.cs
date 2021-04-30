using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PagetechsLMS.WebAndApi.IdentityExtension
{
    public class TokenRequestValidtor : ICustomTokenRequestValidator
    {
        public Task ValidateAsync(CustomTokenRequestValidationContext context)
        {
            return Task.CompletedTask;
        }
    }
}
