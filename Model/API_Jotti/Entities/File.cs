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
    public class File
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "size")]
        public int Size { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "firstSeen")]
        public DateTime FirstSeen { get; set; }

        [JsonProperty(PropertyName = "hashes")]
        public Hashes Hashes { get; set; } 
    }
}
