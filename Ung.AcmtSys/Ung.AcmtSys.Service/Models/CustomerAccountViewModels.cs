using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Ung.AcmtSys.Business;

namespace Ung.AcmtSys.Service.Models
{
    public class CustomerAccount
    {
        [JsonProperty(PropertyName = "account_number")]
        public string AccountNumber { get; set; }

        [JsonProperty(PropertyName = "account_name")]
        public string AccountName { get; set; }

        public CustomerAccount(Account account)
        {
            AccountNumber = account.AccountNumber;
            AccountName = account.AccountName;
        }
    }
}