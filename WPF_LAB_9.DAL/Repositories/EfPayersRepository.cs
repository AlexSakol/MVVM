using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WPF_LAB_9.Domain.Interfaces;
using WPF_LAB_9.Domain.Entities;
using WPF_LAB_9.DAL.Data;

namespace WPF_LAB_9.DAL.Repositories
{
    public class EfPayersRepository : IRepository<Payer>
    {
        private readonly DbSet<Payer> payers;
        public EfPayersRepository(PayContext payContext)
        {
            payers = payContext.Payers;
        }
        public void Create(Payer entity)
        {
            payers.Add(entity);
        }
        public bool Delete(int id)
        {
            var payer = payers.Find(id);
            if (payer == null) return false;
            payers.Remove(payer);
            return true;
        }
        public IQueryable<Payer> Find(Expression<Func<Payer, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Payer>> FindAsync(Expression<Func<Payer, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Payer Get(int id, params string[] includes)
        {
            IQueryable<Payer> query = payers;
            foreach (var include in includes)
                query = query.Include(include);
            return query.First(p => p.PayerId == id);
        }
        public IQueryable<Payer> GetAll()
        {
            return payers.AsQueryable();
        }
        public void Update(Payer entity)
        {
            payers.Update(entity);
        }  
    }
}
