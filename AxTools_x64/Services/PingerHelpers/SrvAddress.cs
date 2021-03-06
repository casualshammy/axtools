﻿using Newtonsoft.Json;

namespace AxTools.Services.PingerHelpers
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class SrvAddress
    {
        [JsonProperty(Order = 0, PropertyName = "IP")]
        internal string Ip;

        [JsonProperty(Order = 1, PropertyName = "Port")]
        internal int Port;

        [JsonProperty(Order = 2, PropertyName = "Description")]
        internal string Description;

        [JsonConstructor]
        internal SrvAddress(string ip, int port, string description)
        {
            Ip = ip;
            Port = port;
            Description = description;
        }
    }
}