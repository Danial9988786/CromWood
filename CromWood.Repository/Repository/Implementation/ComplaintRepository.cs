using CromWood.Data.Context;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CromWood.Data.Repository.Implementation
{
    public class ComplaintRepository:Repository<Complaint>, IComplaintRepository
    {
        public ComplaintRepository(CromwoodContext context) : base(context) { }

        public async Task<IEnumerable<Complaint>> GetComplaints()
        {
           
            return await _context.Complaints.Include(x=>x.ComplaintCategory).Include(x=>x.ComplaintNature).Include(x=>x.Tenant).ToListAsync();
        }

        public async Task<Complaint> GetComplaintById(Guid id)
        {
            return await _context.Complaints.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Complaint> GetComplaintViewById(Guid id)
        {
            return await _context.Complaints.Include(x=>x.ComplaintNature).Include(x=>x.ComplaintCategory).Include(x=>x.ComplaintType).Include(x=>x.Tenant).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> AddModifyComplaint(Complaint complaint)
        {
            try
            {
                if (complaint.Id == Guid.Empty)
                {
                    complaint.Status = "Open";
                    await _context.Complaints.AddAsync(complaint);
                }
                else
                {
                    _context.Complaints.Update(complaint);
                }
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> DeleteComplaint(Guid id)
        {
            try
            {
                var complaint = await _context.Complaints.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                _context.Complaints.Remove(complaint);
                await _context.SaveChangesAsync();
                return complaint.FileUrl;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<ComplaintComment>> GetComplaintComments(Guid complaintId)
        {

            return await _context.ComplaintComments.Where(x=>x.ComplaintId == complaintId).ToListAsync();
        }

        public async Task<ComplaintComment> GetComplaintCommentById(Guid id)
        {
            return await _context.ComplaintComments.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> AddModifyComplaintComment(ComplaintComment comment)
        {
            try
            {
                if (comment.Id == Guid.Empty)
                {
                    await _context.ComplaintComments.AddAsync(comment);
                }
                else
                {
                    _context.ComplaintComments.Update(comment);
                }
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> DeleteComplaintComment(Guid id)
        {
            try
            {
                var comment = await _context.ComplaintComments.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                _context.ComplaintComments.Remove(comment);
                await _context.SaveChangesAsync();
                return comment.FileUrl;
            }
            catch
            {
                throw;
            }
        }

    }
}
