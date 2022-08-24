using Newtonsoft.Json;
using System;

namespace EStore.Controllers
{
    public class JsonObject
    {
        [JsonProperty("user_id")]
        public Int64 UserId { get; set; }

        [JsonProperty("orders")]
        public OrdersInfo OrdersInfo { get; set; }
    }

    public class OrdersInfo
    {
        [JsonProperty("sum")]
        public Int64 Sum { get; set; }

        [JsonProperty("order_number")]
        public Int64 OrderNumber { get; set; }
    }
}

