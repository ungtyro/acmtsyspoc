using System;
using System.Linq;

namespace Ung.AcmtSys.Business
{
    public class Bank
    {
        public Customer GetCustomer(AcmtSysDbEntities context, string personalCardId)
        {
            return context.Customers.FirstOrDefault(x => x.PersonalCardId == personalCardId);
        }

        public void AddPersonalCustomer(AcmtSysDbEntities context, Customer customer)
        {
            customer.CustomerType = CustomerType.Personal.ToString();
            customer.RegisterDateUTC = DateTime.UtcNow;
            customer.Status = CustomerStatusType.Active.ToString();
            context.Customers.Add(customer);
            context.SaveChanges();
        }

        public bool IsCustomerAlreadyExist(AcmtSysDbEntities context, string personalCardId)
        {
            return context.Customers.Any(x => x.PersonalCardId == personalCardId);
        }
    }
}
