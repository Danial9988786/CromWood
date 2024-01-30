using System.Linq.Expressions;

namespace CromWood.Data.Repository.Interface
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(Guid Id);
        Task<string> GetFilterConiditon(Guid filterId);
        T UpdateAsync(T item);
        Task<T> DeleteAsync(Guid id);
        Task<T> AddAsync(T item);
    }
}
