using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Ung.AcmtSys.Business.Models;

namespace Ung.AcmtSys.Service.Models
{
    public class CreateCustomerBindingModel
    {

        [JsonProperty(PropertyName = "customer")]
        public CustomerModel Customer { get; set; }
    }

    public class GetCustomerBindingModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Maximum of 50 characters")]
        public string PersonalCardId { get; set; }
    }
}