using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowScannerService.Model.API_Jotti.Entities
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]

    public class Hashes
    {
        [JsonProperty(PropertyName = "md5")]
        public string MD5 { get; set; }

        [JsonProperty(PropertyName = "sha1")]
        public string Sha1 { get; set; }

        [JsonProperty(PropertyName = "sha256")]
        public string Sha256 { get; set; }

        [JsonProperty(PropertyName = "sha512")]
        public string Sha512 { get; set; }

    }
}
