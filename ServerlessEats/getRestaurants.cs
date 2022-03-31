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
    public static class GetRestaurants
    {
        [FunctionName("GetRestaurants")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName: "ServerlessEats",
                collectionName: "Restaurants",
                ConnectionStringSetting = "CosmosDBConnection",
                SqlQuery = "SELECT * FROM c order by c.restaurant.name desc")]
                IEnumerable<Restaurant> restaurants,
            ILogger log)
        {
            log.LogInformation("Fetching Restaurants");

            try{               
                return new OkObjectResult(restaurants);
            }
            catch (Exception ex){
                log.LogError("Poblem accepting the request.");
                log.LogError(ex.ToString());
                return new BadRequestObjectResult("Poblem accepting the request");
            }
        }
    }
}
