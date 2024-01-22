using CromWood.Data.Context;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CromWood.Data.Repository.Implementation
{
    public class NoticeClaimsRepository : Repository<Notice>, INoticeClaimsRepository
    {
        public NoticeClaimsRepository(CromwoodContext context) : base(context) { }

        public async Task<IEnumerable<Notice>> GetNotices(bool archived = false)
        {
            if(archived==false) {
                return await _context.Notices.Include(x=>x.Concern).Include(x=>x.Section).Include(x=>x.Tenant).Where(x => x.Archived== false || x.Archived == null).ToListAsync();
            }
            return await _context.Notices.Include(x => x.Concern).Include(x => x.Section).Include(x => x.Tenant).Where(x => x.Archived==true).ToListAsync();

        }

        public async Task<Notice> GetNoticeById(Guid id)
        {
            return await _context.Notices.Include(x=>x.Tenant).Include(x=>x.Concern).Include(x=>x.Section).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> AddModifyNotice(Notice notice)
        {
            try
            {
                if (notice.Id == Guid.Empty)
                {
                    await _context.Notices.AddAsync(notice);
                }
                else
                {
                    _context.Notices.Update(notice);
                }
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> DeleteNotice(Guid id)
        {
            try
            {
                var notice = await _context.Notices.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                _context.Notices.Remove(notice);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> ArchiveNotice(Guid id)
        {
            try
            {
                var notice = await _context.Notices.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                notice.Archived = true;
                _context.Notices.Update(notice);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }


        public async Task<IEnumerable<Claim>> GetClaims()
        {
            return await _context.Claims.Include(x=>x.Tenant).Include(x=>x.ClaimType).Include(x=>x.Court).ToListAsync();
        }

        public async Task<Claim> GetClaimById(Guid id)
        {
            return await _context.Claims.Include(x=>x.Tenant).Include(x => x.ClaimType).Include(x=>x.Court).FirstOrDefaultAsync(x => x.Id == id);

        }
        public async Task<int> AddModifyClaim(Claim claim)
        {
            try
            {
                if (claim.Id == Guid.Empty)
                {
                    await _context.Claims.AddAsync(claim);
                }
                else
                {
                    _context.Claims.Update(claim);
                }
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> DeleteClaim(Guid id)
        {
            try
            {
                var claim = await _context.Claims.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                _context.Claims.Remove(claim);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }
    }
}
