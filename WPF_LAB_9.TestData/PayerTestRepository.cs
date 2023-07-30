using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WPF_LAB_9.Domain.Entities;
using WPF_LAB_9.Domain.Interfaces;

namespace WPF_LAB_9.TestData
{
    public class PayerTestRepository : IRepository<Payer>
    {
        private readonly List<Payer> payers;

        public PayerTestRepository(List<Payer> payers)
        {
            this.payers = payers;
            SetupData();
        }

        private void SetupData()
        {
            int s = 1;
            Random rnd = new Random();
            for (int i = 1; i <= 5; i++)
            {
                var payer = new Payer
                {                    
                    DateOfBirth = DateTime.Now - TimeSpan.FromDays(rnd.Next(6000, 20000)),
                    PayerName = $"Плательщик {i}",
                    PayerId = i
                };
                var payments = new List<Payment>();
                for (int j = 0; j < 10; j++)
                {
                    payments.Add(new Payment
                    {
                        PaymentId = i,
                        PaymentDate = DateTime.Now + TimeSpan.FromDays(rnd.Next(10, 20)),
                        PaymentName = $"Платеж {s}",
                        Price = rnd.Next(1000, 5000),
                        PayerId = s,
                    });
                    s++;                   
                }
                payer.Payments = payments;
                payers.Add(payer);
            }
        }
        public void Create(Payer entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Payer> Find(System.Linq.Expressions.Expression<Func<Payer, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Payer Get(int id, params string[] includes)
        {
            return payers.FirstOrDefault(p => p.PayerId == id);
        }

        public IQueryable<Payer> GetAll()
        {
            return payers.AsQueryable();
        }

        public void Update(Payer entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Payer>> FindAsync(Expression<Func<Payer, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
