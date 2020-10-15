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
    public class ScanJob
    {
        [JsonProperty(PropertyName = "id")]
        public string ID { get; set; }

        [JsonProperty(PropertyName = "webUrl")]
        public string WebUrl { get; set; }

        [JsonProperty(PropertyName = "takenOn")]
        public DateTime TakenOn { get; set; }

        [JsonProperty(PropertyName = "scannersRun")]
        public int ScannersRun { get; set; }

        [JsonProperty(PropertyName = "scannersDetected")]
        public int ScannersDetected { get; set; }

        [JsonProperty(PropertyName = "scannerResults")]
        public IEnumerable<ScannerResult> ScannerResults { get; set; }

        [JsonProperty(PropertyName = "metaData")]
        public IEnumerable<MetaData> MetaData { get; set; }

    }
}
