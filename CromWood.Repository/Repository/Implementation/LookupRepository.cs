using CromWood.Data.Context;
using CromWood.Data.Repository.Interface;

namespace CromWood.Data.Repository.Implementation
{
    public class LookupRepository<T> : Repository<T>, ILookupRepository<T> where T : class
    {

        public LookupRepository(CromwoodContext context) : base(context)
        {
        }
    }
}
