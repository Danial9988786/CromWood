using CromWood.Business.Helper;
using CromWood.Business.Models.ViewModel;
using CromWood.Business.Models;

namespace CromWood.Business.Services.Interface
{
    public interface IComplaintService
    {
        public Task<AppResponse<IEnumerable<ComplaintViewModel>>> GetComplaints();
        public Task<AppResponse<ComplaintModel>> GetComplaintById(Guid id);
        public Task<AppResponse<ComplaintViewModel>> GetComplaintViewById(Guid id);
        public Task<AppResponse<int>> AddModifyComplaint(ComplaintModel complaint);
        public Task<AppResponse<int>> DeleteComplaint(Guid id);
        public Task<AppResponse<IEnumerable<ComplaintCommentViewModel>>> GetComplaintComments(Guid coplaintId);
        public Task<AppResponse<ComplaintCommentModel>> GetComplaintCommentById(Guid id);
        public Task<AppResponse<int>> AddModifyComplaintComment(ComplaintCommentModel comment);
        public Task<AppResponse<int>> DeleteComplaintComment(Guid id);

    }
}
