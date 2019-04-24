#r "Newtonsoft.Json"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

public static async Task<IActionResult> Run(HttpRequest req, ILogger log)
{
    log.LogInformation("C# HTTP trigger function processed a request.");

    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    dynamic registrationRequest = JsonConvert.DeserializeObject<RegistrationRequest> (requestBody);
    var email = registrationRequest?.Email;
    var userName = registrationRequest?.UserName;

    return (email != null && userName != null)
        ? (ActionResult)new OkObjectResult($"Hello, {userName} with email {email}")
        : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
}

public class RegistrationRequest
{
    public string Email { get; set; }   
    public string UserName {get;set;}
}
