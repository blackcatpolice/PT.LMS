using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PagetechsLMS.WebAndApi.IdentityExtension
{
    public class ResourceValidator : IResourceValidator
    {
        public Task<ResourceValidationResult> ValidateRequestedResourcesAsync(ResourceValidationRequest request)
        {
            var resource = new IdentityServer4.Models.Resources();
            resource.OfflineAccess = true;
            resource.IdentityResources.Add(new IdentityServer4.Models.IdentityResource
            {
                Name = "openid",
                DisplayName = "openid"
            });
            resource.IdentityResources.Add(new IdentityServer4.Models.IdentityResource
            {
                Name = "profile",
                DisplayName = "profile"
            });

            resource.ApiResources.Add(new IdentityServer4.Models.ApiResource
            {
                Name = "PagetechsLMS.WebAndApiAPI",
                DisplayName = "PagetechsLMS.WebAndApiAPI"
            });

            resource.ApiScopes.Add(new IdentityServer4.Models.ApiScope
            {
                Name = "PagetechsLMS.WebAndApiAPI",
                DisplayName = "PagetechsLMS.WebAndApiAPI"
            });
            var result = new ResourceValidationResult(resource);

            return Task.FromResult(result);
        }
    }
}
