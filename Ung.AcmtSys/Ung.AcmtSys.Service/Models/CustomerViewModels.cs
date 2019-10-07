using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Ung.AcmtSys.Business;
using System.ComponentModel.DataAnnotations;

namespace Ung.AcmtSys.Service.Models
{
    public class CustomerModel
    {
        [Required]
        [JsonProperty(PropertyName = "customer_id")]
        public Guid CustomerId { get; set; }

        [Required]
        [StringLength(140, ErrorMessage = "Maximum of 140 characters")]
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(140, ErrorMessage = "Maximum of 140 characters")]
        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Maximum of 50 characters")]
        [JsonProperty(PropertyName = "personal_card_id")]
        public string PersonalId { get; set; }

        [JsonProperty(PropertyName = "birth_date")]
        public int? Birthdate { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Maximum of 10 characters")]
        [JsonProperty(PropertyName = "sex")]
        public string Sex { get; set; }

        [JsonProperty(PropertyName = "address")]
        public AddressModel Address { get; set; }

        public CustomerModel(Customer customer)
        {
            CustomerId = customer.CustomerId;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            PersonalId = customer.PersonalCardId;
            Birthdate = customer.Birthdate;
            Sex = customer.Sex;
        }

        public CustomerModel()
        {
        }

        public Customer CreateNewCustomerObject()
        {
            return new Customer
            {
                CustomerId = CustomerId,
                FirstName = FirstName,
                LastName =  LastName,
                PersonalCardId = PersonalId,
                Birthdate = Birthdate,
                Sex = Sex
            };
        }
    }

    public class AddressModel
    {
        [JsonProperty(PropertyName = "address_type")]
        public string AddressType { get; set; }

        [StringLength(140, ErrorMessage = "Maximum of 140 characters")]
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [StringLength(140, ErrorMessage = "Maximum of 140 characters")]
        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [StringLength(140, ErrorMessage = "Maximum of 140 characters")]
        [JsonProperty(PropertyName = "postal_code")]
        public string PostalCode { get; set; }

        [StringLength(140, ErrorMessage = "Maximum of 140 characters")]
        [JsonProperty(PropertyName = "country_name")]
        public string CountryName { get; set; }

        [StringLength(140, ErrorMessage = "Maximum of 140 characters")]
        [JsonProperty(PropertyName = "address_one")]
        public string AddressLineOne { get; set; }

        [StringLength(140, ErrorMessage = "Maximum of 140 characters")]
        [JsonProperty(PropertyName = "address_two")]
        public string AddressLineTwo { get; set; }

        [StringLength(140, ErrorMessage = "Maximum of 140 characters")]
        [JsonProperty(PropertyName = "address_three")]
        public string AddressLineThree { get; set; }
    }
}