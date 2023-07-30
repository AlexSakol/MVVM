using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WPF_LAB_9.Domain.Entities;
using WPF_LAB_9.Domain.Interfaces;

namespace WPF_LAB_9.TestData
{
    public class PaymentTestRepository : IRepository<Payment>
    {
        private readonly List<Payment> payments;

        public PaymentTestRepository(List<Payment> payments)
        {
            this.payments = payments;
        }

        public void Create(Payment entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Payment> Find(Expression<Func<Payment, bool>> predicate)
        {
            Func<Payment, bool> filter = predicate.Compile();
            return payments.Where(filter).AsQueryable();
        }

        public Task<IEnumerable<Payment>> FindAsync(Expression<Func<Payment, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Payment Get(int id, params string[] includes)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Payment> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Payment entity)
        {
            throw new NotImplementedException();
        }
    }
}
