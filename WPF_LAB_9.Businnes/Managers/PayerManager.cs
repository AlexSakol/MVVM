using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WPF_LAB_9.Domain.Interfaces;
using WPF_LAB_9.Domain.Entities;


namespace WPF_LAB_9.Businnes.Managers
{
    public class PayerManager: BaseManager
    {
        public PayerManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public IEnumerable<Payer> Payers
        {
            get => payerRepository.GetAll();
        }
        public Payer GetById(int id) => payerRepository.Get(id);
        public Payer CreatePayer(Payer payer)
        {
            payerRepository.Create(payer);
            unitOfWork.SaveChanges();
            return payer;
        }
        public void AddRange(List<Payer> payers)
        {
            payers.ForEach(p => payerRepository.Create(p));
            unitOfWork.SaveChanges();
        }
        public bool DeletePayer(int id)
        {
            var result = payerRepository.Delete(id);
            if (!result) return false;
            unitOfWork.SaveChanges();
            return true;
        }
        public void UpdatePayer(Payer payer)
        {
            payerRepository.Update(payer);
            unitOfWork.SaveChanges();
        }
        public void AddPaymentToPayer(Payment payment, int payerId)
        {
            var payer = payerRepository.Get(payerId);
            payment.PayerId = payer.PayerId;           
            if (payment.PaymentId <= 0)
                paymentRepository.Create(payment);
            else paymentRepository.Update(payment);
            unitOfWork.SaveChanges();
        }

        public void RemovePaymentFromPayer(Payment payment, int payerId)
        {
            var payer = payerRepository.Get(payerId, "Payments");
            payer.Payments.Remove(payment);
            payment.Price = 0;
            payerRepository.Update(payer);
            paymentRepository.Update(payment);
            unitOfWork.SaveChanges();
        }
        public ICollection<Payment> GetPaymentsOfPayer(int payerId) =>
            paymentRepository
            .Find(p => p.PayerId == payerId)
            .ToList();

        public async Task<IEnumerable<Payment>> GetPaymentsOfPayerAsync(int payerId) =>
            await paymentRepository.FindAsync(p => p.PayerId == payerId);
    }
}
