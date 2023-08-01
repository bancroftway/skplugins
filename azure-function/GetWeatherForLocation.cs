using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace sk_chatgpt_azure_function
{
    public class GetWeatherForLocation
    {
        private readonly ILogger _logger;

        public GetWeatherForLocation(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GetWeatherForLocation>();
        }

        [OpenApiOperation(operationId: "GetWeatherForLocation", tags: new[] {"ExecuteFunction"}, Description = "Get the weather for a given location")]
        [OpenApiParameter(name: "location", Description ="Location, such as city or zip code", Required =true, In =Microsoft.OpenApi.Models.ParameterLocation.Query)]
        [OpenApiResponseWithBody(statusCode:HttpStatusCode.OK, contentType:"text/plain", bodyType:typeof(string), Description ="Weather at the given location")]
        [Function("GetWeatherForLocation")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var location = req.Query["location"];

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            if ( location == "San Ramon")
            {
                response.WriteString("Cold");
            }
            else
            {
                response.WriteString("Hot");
            }         
            return response;
        }
    }
}
