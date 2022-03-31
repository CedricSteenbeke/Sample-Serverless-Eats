using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace fss.ServerlessEats
{
    public static class GetMenu
    {
        [FunctionName("GetMenu")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "restaurants/{id}")] HttpRequest req,
            [CosmosDB(
                databaseName: "ServerlessEats",
                collectionName: "Restaurants",
                ConnectionStringSetting = "CosmosDBConnection",
                Id = "{id}",
                PartitionKey = "{id}")] Restaurant restaurant,
            ILogger log)
        {
            log.LogInformation("Fetching menu's");

            try{               
                return new OkObjectResult(restaurant);
            }
            catch (Exception ex){
                log.LogError("Poblem accepting the request.");
                log.LogError(ex.ToString());
                return new BadRequestObjectResult("Poblem accepting the request");
            }
        }
    }
}
