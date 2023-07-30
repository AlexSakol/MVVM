using System;

namespace WPF_LAB_9.Domain.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public string PaymentName { get; set; }
        public decimal Price { get; set; }
        public DateTime PaymentDate { get; set; }
        public string ImageFileName { get; set; }
        public int PayerId { get; set; }
        public Payer Payer { get; set; }
    }
}
