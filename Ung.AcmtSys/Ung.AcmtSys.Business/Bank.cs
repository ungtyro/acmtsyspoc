using System;
using System.Linq;

namespace Ung.AcmtSys.Business
{
    public class Bank
    {
        /// <summary>
        /// Provide a customer information.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="personalCardId"></param>
        /// <returns></returns>
        public Customer GetCustomer(AcmtSysDbEntities context, string personalCardId)
        {
            return context.Customers.FirstOrDefault(x => x.PersonalCardId == personalCardId);
        }

        /// <summary>
        /// Create new customer to bank system.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="customer"></param>
        public void AddPersonalCustomer(AcmtSysDbEntities context, Customer customer)
        {
            customer.CustomerType = CustomerType.Personal.ToString();
            customer.RegisterDateUTC = DateTime.UtcNow;
            customer.Status = CustomerStatusType.Active.ToString();
            context.Customers.Add(customer);
            context.SaveChanges();
        }


        /// <summary>
        /// Check Personal card ID.
        /// If not yet, the system will return true.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="personalCardId"></param>
        /// <returns></returns>
        public bool IsCustomerAlreadyExist(AcmtSysDbEntities context, string personalCardId)
        {
            return context.Customers.Any(x => x.PersonalCardId == personalCardId);
        }
    }
}
