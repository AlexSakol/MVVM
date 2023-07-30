using WPF_LAB_9.Domain.Interfaces;
using WPF_LAB_9.TestData;
using Microsoft.Extensions.Configuration;
using System.IO;
using WPF_LAB_9.DAL.Repositories;

namespace WPF_LAB_9.Businnes.Managers
{
    public class ManagersFactory
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly PaymentManager paymentManager;
        private readonly PayerManager payerManager;
        public ManagersFactory()
        {
            unitOfWork = new TestUnitOfWork();
        }
        public ManagersFactory(string connStringName)
        {
            var configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json")
             .Build();
            var connString = configuration
            .GetConnectionString(connStringName);
            unitOfWork = new EfUnitOfWork(connString);
        }
        public PaymentManager GetPaymentManager()
        {
            return paymentManager
            ?? new PaymentManager(unitOfWork);
        }
        public PayerManager GetPayerManager()
        {
            return payerManager
            ?? new PayerManager(unitOfWork);
        }
    }
}
