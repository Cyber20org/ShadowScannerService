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

    public class Result
    {

        [JsonProperty(PropertyName = "file")]
        public File File { get; set; }

        [JsonProperty(PropertyName = "mostRecentScanJob")]
        public ScanJob MostRecentScanJob { get; set; }
    }
}
