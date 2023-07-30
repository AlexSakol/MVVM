using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WPF_LAB_9.Domain.Interfaces;
using WPF_LAB_9.Domain.Entities;

namespace WPF_LAB_9.Businnes.Managers
{
    public class PaymentManager: BaseManager
    {
        public PaymentManager(IUnitOfWork untOfWork) : base(untOfWork)
        {
        }
        #region bacic CRUD operations
        public bool DeletePayment(int id)
        {
            var result = paymentRepository.Delete(id);
            if (!result) return false;
            unitOfWork.SaveChanges();
            return true;
        }
        public IEnumerable<Payment> FindPayment(Expression<Func<Payment, bool>> predicate) =>
        paymentRepository.Find(predicate);
        public Payment GetPaymentById(int id) =>
        paymentRepository.Get(id);
        public IEnumerable<Payment> GetAllPayments() => paymentRepository.GetAll();
        public void UpdatePayment(Payment payment)
        {
            paymentRepository.Update(payment);
            unitOfWork.SaveChanges();
        }
        #endregion
    }
}
