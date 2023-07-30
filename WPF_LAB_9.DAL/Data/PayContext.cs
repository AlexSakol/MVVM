using WPF_LAB_9.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace WPF_LAB_9.DAL.Data
{
    public class PayContext: DbContext
    {
        public PayContext(DbContextOptions options): base(options)
        {
        }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Payer> Payers { get; set; }
    }
}
