using Newtonsoft.Json;
using ShadowScannerService.Model.API_Jotti.Entities;
using ShadowScannerService.Model.API_Jotti.Helper;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;



namespace ShadowScannerService.Model.API_Jotti
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Response
    {
        //[JsonConverter(typeof(DictionaryConverter))]
        [JsonProperty(PropertyName = "results")]
        public IDictionary<string, Result> Results { get; set; } 

        [JsonProperty(PropertyName = "noresults")]
        public IEnumerable<string> NoResults { get; set; }

        [JsonProperty(PropertyName = "skipped")]
        public IEnumerable<string> Skipped { get; set; }


    }
}
