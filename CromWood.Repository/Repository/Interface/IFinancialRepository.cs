using CromWood.Data.Entities;

namespace CromWood.Data.Repository.Interface
{
    public interface IFinancialRepository
    {
        public Task<ICollection<TenancyStatement>> GetRentStatements(Guid id);
    }
}
