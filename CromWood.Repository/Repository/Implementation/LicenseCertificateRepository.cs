using CromWood.Data.Context;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace CromWood.Data.Repository.Implementation
{
    public class LicenseCertificateRepository : Repository<LicenseCertificate>, ILicenseCertificateRepository
    {
        public LicenseCertificateRepository(CromwoodContext context) : base(context)
        {
        }

        public async Task<IEnumerable<LicenseCertificate>> GetAllLicenseCertificatesList(Guid filterId)
        {
            if (filterId != Guid.Empty)
            {
                var condition = await GetFilterConiditon(filterId);
                var result = await _context.LicenseCertificates.Where(condition).Include(x => x.Property).ThenInclude(x => x.Asset).Include(x => x.LicenseCertificateType).ToListAsync();
                return result;
            }
            return await _context.LicenseCertificates.Include(x=>x.Property).ThenInclude(x=>x.Asset).Include(x=>x.LicenseCertificateType).ToListAsync();
        }

        public async Task<IEnumerable<LicenseCertificate>> GetAllLicenseCertificates(Guid propertyId)
        {
            return await _context.LicenseCertificates.Include(x => x.Property).ThenInclude(x => x.Asset).Include(x => x.LicenseCertificateType).Where(x => x.PropertyId == propertyId).ToListAsync();
        }

        public async Task<LicenseCertificate> GetLicenseCertificateById(Guid Id)
        {
            return await _context.LicenseCertificates.Include(x => x.Property).ThenInclude(x => x.Asset).Include(x => x.LicenseCertificateType).FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<int> AddLicense(LicenseCertificate license)
        {
            try
            {
                license.Id = Guid.NewGuid();
                await _context.LicenseCertificates.AddAsync(license);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> EditLicense(LicenseCertificate license)
        {
            try
            {
                _context.LicenseCertificates.Update(license);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> ArchieveLicense(Guid Id)
        {
            try
            {
                var license = await _context.LicenseCertificates.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
                license.Archieved = true;
                _context.LicenseCertificates.Update(license);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> DeleteLicense(Guid Id)
        {
            try
            {
                var license = await _context.LicenseCertificates.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
                _context.LicenseCertificates.Remove(license);
                await _context.SaveChangesAsync();
                return license.DocUrl;
            }
            catch
            {
                throw;
            }
        }
    }
}
