using System;
using System.Collections.Generic;

namespace WPF_LAB_9.Domain.Entities
{
    public class Payer
    {
        public int PayerId { get; set; }
        public string PayerName { get; set;}
        public DateTime DateOfBirth { get; set; }      
        public int PaymentId { get; set; } 
        public ICollection<Payment> Payments { get; set; }
    }
}
