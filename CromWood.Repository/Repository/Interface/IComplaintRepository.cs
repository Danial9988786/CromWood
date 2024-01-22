using CromWood.Data.Entities;

namespace CromWood.Data.Repository.Interface
{
    public interface IComplaintRepository
    {
        public Task<IEnumerable<Complaint>> GetComplaints();
        public Task<Complaint> GetComplaintById(Guid id);
        public Task<Complaint> GetComplaintViewById(Guid id);
        public Task<int> AddModifyComplaint(Complaint comlaint);
        public Task<string> DeleteComplaint(Guid id);
        public Task<IEnumerable<ComplaintComment>> GetComplaintComments(Guid complaintId);
        public Task<ComplaintComment> GetComplaintCommentById(Guid id);
        public Task<int> AddModifyComplaintComment(ComplaintComment comment);
        public Task<string> DeleteComplaintComment(Guid id);
    }
}
