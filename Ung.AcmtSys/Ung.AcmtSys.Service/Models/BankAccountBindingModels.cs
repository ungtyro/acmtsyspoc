using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Ung.AcmtSys.Service.Models
{
    public class DepositBindingModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Maximum of 50 characters")]
        [JsonProperty(PropertyName = "account_number")]
        public string AccountNumber { get; set; }


        [Required]
        [JsonProperty(PropertyName = "deposit_amount")]
        public decimal DepositAmount { get; set; }
    }

    public class DirectTransferBindingModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Maximum of 50 characters")]
        [JsonProperty(PropertyName = "source_account_number")]
        public string SourceAccountNumber { get; set; }


        [Required]
        [StringLength(50, ErrorMessage = "Maximum of 50 characters")]
        [JsonProperty(PropertyName = "destination_account_number")]
        public string DestinationAccountNumber { get; set; }


        [Required]
        [RegularExpression(@"^[+]?([.]\d+|\d+[.]?\d*)$", ErrorMessage = "Invalid price")]
        [JsonProperty(PropertyName = "transfer_amount")]
        public decimal TransferAmount { get; set; }
    }
}