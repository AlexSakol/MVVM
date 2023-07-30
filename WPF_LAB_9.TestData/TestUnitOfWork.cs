using System.Collections.Generic;
using WPF_LAB_9.Domain.Interfaces;
using WPF_LAB_9.Domain.Entities;


namespace WPF_LAB_9.TestData
{
    public class TestUnitOfWork : IUnitOfWork
    {
        private IRepository<Payer> payersRepository;
        private IRepository<Payment> paymentsRepository;
        private List<Payer> payers;
        private List<Payment> payments;
        public TestUnitOfWork()
        {
            payers = new List<Payer>();
            payments = new List<Payment>();
            payersRepository = new PayerTestRepository(payers);
            foreach (var payer in payers)
            payments.AddRange(payer.Payments);
            paymentsRepository = new PaymentTestRepository(payments);
        }
        public IRepository<Payment> PaymentsRepository =>
        paymentsRepository;
        public IRepository<Payer> PayersRepository =>
        payersRepository;
        public void SaveChanges()
        {
        }
    }
}
