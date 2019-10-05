using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ung.AcmtSys.Business.Models
{
    public class CustomerModel
    {
        [JsonProperty(PropertyName = "customer_id")]
        public Guid CustomerId { get; set; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "personal_id")]
        public string PersonalId { get; set; }

        [JsonProperty(PropertyName = "birth_date")]
        public int Birthdate { get; set; }

        [JsonProperty(PropertyName = "sex")]
        public string Sex { get; set; }

        [JsonProperty(PropertyName = "address")]
        public AddressModel AddressModel { get; set; }
    }

    public class AddressModel
    {
        [JsonProperty(PropertyName = "address_type")]
        public string AddressType { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "postal_code")]
        public string PostalCode { get; set; }

        [JsonProperty(PropertyName = "country_name")]
        public string CountryName { get; set; }

        [JsonProperty(PropertyName = "address_one")]
        public string AddressLineOne { get; set; }

        [JsonProperty(PropertyName = "address_two")]
        public string AddressLineTwo { get; set; }

        [JsonProperty(PropertyName = "address_three")]
        public string AddressLineThree { get; set; }
    }
}
