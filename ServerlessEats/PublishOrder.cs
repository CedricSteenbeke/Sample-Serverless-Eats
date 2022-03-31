using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;

namespace fss.ServerlessEats
{
    public static class PublishOrder
    {
        [FunctionName("PublishOrder")]
        public static async void Run([CosmosDBTrigger(
            databaseName: "ServerlessEats",
            collectionName: "Orders",
            ConnectionStringSetting = "CosmosDBConnection",
            LeaseCollectionName = "leases")]IReadOnlyList<Document> input, 
            [ServiceBus("OrderBus", Connection = "ServiceBusConnection")] IAsyncCollector<dynamic> outputServiceBus,
            ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents modified " + input.Count);
                log.LogInformation("First document Id " + input[0].Id);

                var orderObject = JsonConvert.DeserializeObject<Order>(input[0].ToString());

                var sbMessage = new ServiceBusMessage(input[0].ToString());
                sbMessage.Subject = orderObject.restaurantName;

                await outputServiceBus.AddAsync(sbMessage);
            }
        }
    }
}
