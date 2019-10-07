using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ung.AcmtSys.Business.Models;

namespace Ung.AcmtSys.Service.Models
{
    public class CreateCustomerBindigModel
    {
        [JsonProperty(PropertyName = "branch_id")]
        public string BranchId { get; set; }

        [JsonProperty(PropertyName = "customer")]
        public CustomerModel Customer { get; set; }
    }
}