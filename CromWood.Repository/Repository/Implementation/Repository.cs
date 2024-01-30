using CromWood.Data.Context;
using CromWood.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CromWood.Data.Repository.Implementation
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly CromwoodContext _context;
        public Repository(CromwoodContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync().ConfigureAwait(false);
        }

        public async Task<T> GetByIdAsync(Guid Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }

        public async Task<string> GetFilterConiditon(Guid filterId)
        {
            var filter = await _context.Filters.Include(x => x.AndCondition).Include(x => x.OrCondition).FirstOrDefaultAsync(x => x.Id == filterId);
            var condition = "x=>";
            condition += string.Join(" && ", filter.AndCondition.Select(c => c.Condition));
            if (filter.OrCondition.Count > 0)
            {
                condition += " && (";
            }
            condition += string.Join(" || ", filter.OrCondition.Select(c => c.Condition));
            if (filter.OrCondition.Count > 0)
            {
                condition += ")";
            }
            return condition;
        }

        public async Task<T> AddAsync(T item)
        {
            try
            {

                await _context.Set<T>().AddAsync(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                string message = ex.ToString();
                return null;
            }
        }

        public T UpdateAsync(T item)
        {
            _context.Update(item);
            _context.SaveChanges();
            return item;
        }

        public async Task<T> DeleteAsync(Guid Id)
        {
            var item = await GetByIdAsync(Id);
            _context.Set<T>().Remove(item);
            await _context.SaveChangesAsync();
            return null;
        }
    }
}
