using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag;
using NSwag.CodeGeneration.TypeScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PageTechsLMS.Admin.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ClientCodeController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Generate()
        {
            string baseUrl = $"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}{this.Request.PathBase.Value.ToString()}";
            OpenApiDocument document = await OpenApiDocument.FromUrlAsync($"{baseUrl}/swagger/v1/swagger.json");

            TypeScriptClientGeneratorSettings settings = new TypeScriptClientGeneratorSettings
            {
                Template = TypeScriptTemplate.Axios,
                PromiseType = PromiseType.Promise,
                HttpClass = HttpClass.Http,
                InjectionTokenType = InjectionTokenType.InjectionToken, 
                BaseUrlTokenName = "Authorization",
                WithCredentials = true,
                ClientBaseClass = "ApiClientBase",
                GenerateClientClasses = true,
                UseTransformOptionsMethod = true,
                UseTransformResultMethod = true,
                GenerateOptionalParameters = true,

            };

            var generator = new TypeScriptClientGenerator(document, settings);
            var code = generator.GenerateFile();

            return Content(code);
        }
    }
}
