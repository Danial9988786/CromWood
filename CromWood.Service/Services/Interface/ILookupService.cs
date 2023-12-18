using CromWood.Business.Helper;

namespace CromWood.Business.Services.Interface
{
    public interface ILookupService<T>
    {
        public Task<AppResponse<IEnumerable<T>>> GetAllItems();
    }
}
