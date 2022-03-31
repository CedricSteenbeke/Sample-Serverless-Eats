using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace fss.ServerlessEats
{
    public static class CreateOrder
    {
        [FunctionName("CreateOrder")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName: "ServerlessEats",
                collectionName: "Orders",
                ConnectionStringSetting = "CosmosDBConnection") ] IAsyncCollector<Order> orderOut,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            try{
                var orderObject = JsonConvert.DeserializeObject<Order>(requestBody as string);
                await orderOut.AddAsync(orderObject);
                return new OkObjectResult(orderOut);
            }catch (Exception ex){
                log.LogError("Poblem accepting the order request.");
                log.LogError(ex.ToString());
                return new BadRequestObjectResult("Poblem accepting the order request");
            }
        }
    }
}
