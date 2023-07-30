using Microsoft.EntityFrameworkCore;
using WPF_LAB_9.Domain.Interfaces;
using WPF_LAB_9.Domain.Entities;
using WPF_LAB_9.DAL.Data;

namespace WPF_LAB_9.DAL.Repositories
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly PayContext context;
        private IRepository<Payment> paymentsRepository;
        private IRepository<Payer> payersRepository;

        public EfUnitOfWork(string connectionString)
        {
            var options = new DbContextOptionsBuilder<PayContext>()
            .UseSqlServer(connectionString)
            .Options;
            context = new PayContext(options);
            context.Database.EnsureCreated();
        }        
        public IRepository<Payment> PaymentsRepository =>
        paymentsRepository ?? new EfPaymentRepository(context);
        public IRepository<Payer> PayersRepository =>
        payersRepository ?? new EfPayersRepository(context);
        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
