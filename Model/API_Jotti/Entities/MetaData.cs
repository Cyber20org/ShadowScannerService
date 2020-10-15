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
    public class MetaData
    {
        [JsonProperty(PropertyName = "metaDataTypeId")]
        public string TypeId { get; set; }

        [JsonProperty(PropertyName = "metaDataTypeDescription")]
        public string TypeDescription { get; set; }

        [JsonProperty(PropertyName = "metaDataProviderId")]
        public string ProviderId { get; set; }

        [JsonProperty(PropertyName = "metaDataProviderName")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "PropertyName")]
        public string Content { get; set; }
    }
}
