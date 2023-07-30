using WPF_LAB_9.Domain.Entities;
using WPF_LAB_9.Domain.Interfaces;

namespace WPF_LAB_9.Businnes.Managers
{
    public class BaseManager
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IRepository<Payment> paymentRepository;
        protected readonly IRepository<Payer> payerRepository;
        public BaseManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            paymentRepository = unitOfWork.PaymentsRepository;
            payerRepository = unitOfWork.PayersRepository;
        }
    }
}
