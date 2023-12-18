using CromWood.Data.Context;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;

namespace CromWood.Data.Repository.Implementation
{
    public class PropertyRepository: Repository<Property>, IPropertyRepository
    {
        public PropertyRepository(CromwoodContext context) : base(context)
        {
        }
    }
}
