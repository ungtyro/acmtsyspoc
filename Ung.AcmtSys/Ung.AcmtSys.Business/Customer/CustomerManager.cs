using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ung.AcmtSys.Business.Models;

namespace Ung.AcmtSys.Business
{
    public class CustomerManager
    {
        private readonly EntitiesHelper _entitiesHelper;
        public CustomerManager()
        {
            _entitiesHelper = new EntitiesHelper();
        }

        public CustomerModel GetCustomer(string personalId)
        {
            CustomerModel customer = null;

            using (var context = new AcmtsysdbEntities())
            {
                var query = from rec in context.Customers
                            where rec.IdCard == personalId
                            select rec;

                var customerObject = _entitiesHelper.FirstOrDefaultReadUncommitted(query);
                if (customerObject != null)
                {
                    customer = new CustomerModel
                    {
                        CustomerId = customerObject.CustomerId,
                        FirstName = customerObject.FirstName,
                        LastName = customerObject.LastName,
                        Birthdate = customerObject.Birthdate,
                        Sex = customerObject.Sex,
                        PersonalId = customerObject.IdCard
                    };

                    var addressQuery = from rec in context.CustomerAddresses
                                       where rec.CustomerId == customerObject.CustomerId
                                       select rec;
                    var addressObject = _entitiesHelper.FirstOrDefaultReadUncommitted(addressQuery);
                    if (addressObject != null)
                    {
                        customer.Address = new AddressModel
                        {
                            AddressType = addressObject.AddressType,
                            City = addressObject.City,
                            State = addressObject.State,
                            CountryName = addressObject.CountryName,
                            PostalCode = addressObject.PostalCode,
                            AddressLineOne = addressObject.AddressLineOne,
                            AddressLineTwo = addressObject.AddressLineTwo,
                            AddressLineThree = addressObject.AddressLineThree
                        };
                    }
                }
            }

            return customer;
        }

        public void CreateCustomer(string branchId, CustomerModel customer)
        {
            var guidBranchId = new Guid(branchId);

            if (customer != null)
            {
                using (var context = new AcmtsysdbEntities())
                {
                    var customerObject = new Customer
                    {
                        CustomerId = Guid.NewGuid(),
                        BranchId = guidBranchId,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Sex = customer.Sex,
                        IdCard = customer.PersonalId,
                        Birthdate = customer.Birthdate,
                        RegisterDateUTC = DateTime.UtcNow,
                        Status = CustomerStatus.ACT.ToString()
                    };

                    context.Customers.Add(customerObject);

                    if (customer.Address != null)
                    {
                        var addressObject = new CustomerAddress
                        {
                            CustomerAddressId = Guid.NewGuid(),
                            CustomerId = customerObject.CustomerId,
                            AddressType = AddressType.HOME.ToString(),
                            City = customer.Address.City,
                            State = customer.Address.State,
                            PostalCode = customer.Address.PostalCode,
                            CountryName = customer.Address.CountryName,
                            AddressLineOne = customer.Address.AddressLineOne,
                            AddressLineTwo = customer.Address.AddressLineTwo,
                            AddressLineThree = customer.Address.AddressLineThree
                        };
                        context.CustomerAddresses.Add(addressObject);
                    }
                    context.SaveChanges();
                }
            }

        }
    }
}
