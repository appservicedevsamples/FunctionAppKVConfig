using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;


namespace FunctionAppStartupLogging
{
    public class Function1
    {
        private readonly IConfiguration _config;

        public Function1(IConfiguration config)
        {
            _config = config;
        }

        [FunctionName("Function1")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("fakesecret1 is: " + _config["fakesecret1"]);

            //disclaimer: don't ever actually return a sensitive secret in an http response - this is just for quick demonstration
            return new OkObjectResult($"fakesecret1 is: " + _config["fakesecret1"]);
        }
    }
}
