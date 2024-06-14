using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class PushSubViewModel
    {
        [JsonProperty("endpoint")]
        public string Endpoint { get; set; }

        [JsonProperty("expirationTime")]
        public object ExpirationTime { get; set; }

        [JsonProperty("keys")]
        public Keys Keys { get; set; }
    }

    public class Keys
    {
        [JsonProperty("p256dh")]
        public string P256dh { get; set; }

        [JsonProperty("auth")]
        public string Auth { get; set; }
    }
}
