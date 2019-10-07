using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Ung.AcmtSys.Service.Models
{
    public class RequestOpenSavingAccount
    {
        [Required]
        [StringLength(50, ErrorMessage = "Maximum of 50 characters")]
        [JsonProperty(PropertyName = "personal_card_id")]
        public string PersonalCardId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Maximum of 50 characters")]
        [JsonProperty(PropertyName = "account_name")]
        public string AccountName { get; set; }
    }
}