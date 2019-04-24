using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Registration
{
    public static class Registration
    {
        [FunctionName("Registration")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<RegistrationRequest>(requestBody);
            var email  = data?.Email;
            var userName = data?.UserName;
            
            return (email != null && userName != null)
                ? (ActionResult)new OkObjectResult($"Hello, {userName}")
                : new BadRequestObjectResult("Invalid registration data");
        }
    }

    public class RegistrationRequest
    {
        public string Email { get; set; }   
        public string UserName { get; set; }
    }
}
