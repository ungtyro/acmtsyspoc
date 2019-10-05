using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Ung.AcmtSys.Business
{
    public class EntitiesHelper
    {
        public EntitiesHelper()
        {

        }

        public List<T> ToListReadUncommitted<T>(IQueryable<T> query)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var toReturn = query.ToList();
                scope.Complete();
                return toReturn;
            }
        }

        public List<T> ToListSerializable<T>(IQueryable<T> query)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.Serializable }))
            {
                var toReturn = query.ToList();
                scope.Complete();
                return toReturn;
            }
        }

        public T FirstReadUncommitted<T>(IQueryable<T> query)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var toReturn = query.First();
                scope.Complete();
                return toReturn;
            }
        }

        public T FirstOrDefaultReadUncommitted<T>(IQueryable<T> query)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var toReturn = query.FirstOrDefault();
                scope.Complete();
                return toReturn;
            }
        }

        public bool AnyReadUncommitted<T>(IQueryable<T> query)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var toReturn = query.Any();
                scope.Complete();
                return toReturn;
            }
        }
        public int CountReadUncommitted<T>(IQueryable<T> query)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var toReturn = query.Count();
                scope.Complete();
                return toReturn;
            }
        }
        public int SumReadUncommitted(IQueryable<int> query)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var toReturn = query.DefaultIfEmpty(0).Sum();
                scope.Complete();
                return toReturn;
            }
        }
        public int SumReadUncommitted(IQueryable<int?> query)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var toReturn = query.DefaultIfEmpty(0).Sum();
                var result = toReturn ?? 0;
                scope.Complete();
                return result;
            }
        }

        public double AverageReadUncommitted(IQueryable<int?> query)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var toReturn = query.DefaultIfEmpty(0).Average();
                var result = toReturn ?? 0;
                scope.Complete();
                return result;
            }
        }

        public double SumReadUncommitted(IQueryable<double> query)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var toReturn = query.DefaultIfEmpty(0).Sum();
                scope.Complete();
                return toReturn;
            }
        }
        public double SumReadUncommitted(IQueryable<double?> query)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var toReturn = query.DefaultIfEmpty(0).Sum();
                var result = toReturn ?? 0;
                scope.Complete();
                return result;
            }
        }
    }
}
