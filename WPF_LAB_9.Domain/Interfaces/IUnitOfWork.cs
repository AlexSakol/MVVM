using WPF_LAB_9.Domain.Entities;

namespace WPF_LAB_9.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Payer> PayersRepository { get; }
        IRepository<Payment> PaymentsRepository { get; }
        void SaveChanges();
    }
}
