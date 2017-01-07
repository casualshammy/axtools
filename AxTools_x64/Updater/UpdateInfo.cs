﻿using AxTools.Helpers;
using Newtonsoft.Json;

namespace AxTools.Updater
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class UpdateInfo2
    {
        internal static UpdateInfo2 FromJSON(string s)
        {
            return JsonConvert.DeserializeObject<UpdateInfo2>(s);
        }

        [JsonProperty(Order = 0, PropertyName = "Version")]
        internal VersionExt Version;

        //[JsonProperty(Order = 1, PropertyName = "PanicMode")]
        //internal bool PanicMode;

        //[JsonProperty(Order = 2, PropertyName = "DistrZipURL")]
        //internal Uri DistrZipURL;

        //[JsonProperty(Order = 3, PropertyName = "UpdaterZipURL")]
        //internal Uri UpdaterZipURL;

    }
}
