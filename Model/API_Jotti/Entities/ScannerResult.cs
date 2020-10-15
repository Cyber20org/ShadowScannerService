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
    public class ScannerResult
    {
        [JsonProperty(PropertyName = "scannerID")]
        public string ScannerID { get; set; }

        [JsonProperty(PropertyName = "scannerName")]
        public string ScannerName { get; set; }

        [JsonProperty(PropertyName = "scannerLogoUrl")]
        public string ScannerLogoUrl { get; set; }

        [JsonProperty(PropertyName = "signatureFileDate")]
        public DateTime SignatureFileDate { get; set; }

        [JsonProperty(PropertyName = "malwareName")]
        public string MalwareName { get; set; }

    }
}
