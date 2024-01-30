using CromWood.Data.Entities;
using System.Linq.Expressions;

namespace CromWood.Data.Repository.Interface
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(Guid Id);
        Task<string> GetFilterConiditon(Guid filterId);
        Task<int> AddChangeLog(ChangeLog log);
        Task<IEnumerable<ChangeLog>> GetChangeLogsForScreen(string screenName);
        Task<IEnumerable<ChangeLog>> GetChangeLogsForScreenDetail(Guid screenDetailId);
        T UpdateAsync(T item);
        Task<T> DeleteAsync(Guid id);
        Task<T> AddAsync(T item);
    }
}
