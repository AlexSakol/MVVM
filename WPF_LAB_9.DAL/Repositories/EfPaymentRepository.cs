using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WPF_LAB_9.Domain.Interfaces;
using WPF_LAB_9.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WPF_LAB_9.DAL.Data;
using System.Threading;

namespace WPF_LAB_9.DAL.Repositories
{
    public class EfPaymentRepository : IRepository<Payment>
    {
        private readonly PayContext context;
        private readonly DbSet<Payment> payments;
        public EfPaymentRepository(PayContext context)
        {
            this.context = context;
            payments = context.Payments;
        }
        public void Create(Payment payment)
        {
            payments.Add(payment);
        }
        public bool Delete(int id)
        {
            var payment = payments.Find(id);
            if (payment == null) return false;
            if (payment.PaymentId > 0)
            {
             context.Payers.Find(payment.PayerId).Payments.Remove(payment);
            };
            payments.Remove(payment);
            return true;
        }
        public IQueryable<Payment> Find(Expression<Func<Payment, bool>> predicate)
        {
            return payments.Where(predicate);
        }
        public async Task<IEnumerable<Payment>> FindAsync(Expression<Func<Payment, bool>> predicate)
        {
            return await Task.Run(() => {
                Thread.Sleep(2000);
                return payments.Where(predicate);
            });
        }
        public Payment Get(int id, params string[] includes)
        {
            IQueryable<Payment> query = payments;
            foreach (var include in includes)
                query = query.Include(include);
            return query.First(s => s.PaymentId == id);
        }
        public IQueryable<Payment> GetAll()
        {
            return payments.AsQueryable();
        }
        public void Update(Payment payment)
        {
            payments.Update(payment);
        }
    }
}
