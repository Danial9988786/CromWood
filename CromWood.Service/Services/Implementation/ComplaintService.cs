using AutoMapper;
using CromWood.Business.Helper;
using CromWood.Business.Models.ViewModel;
using CromWood.Business.Models;
using CromWood.Business.Services.Interface;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;
using CromWood.Helper;

namespace CromWood.Business.Services.Implementation
{
    public class ComplaintService : IComplaintService
    {
        private readonly IComplaintRepository _complaintRepository;
        private readonly IMapper _mapper;
        private readonly IFileUploader _fileUploader;
        public ComplaintService(IComplaintRepository complaintClaimsRepository, IMapper mapper, IFileUploader fileUploader)
        {
            _complaintRepository = complaintClaimsRepository;
            _mapper = mapper;
            _fileUploader = fileUploader;

        }
        public async Task<AppResponse<IEnumerable<ComplaintViewModel>>> GetComplaints()
        {
            try
            {
                var result = await _complaintRepository.GetComplaints();
                var mappedResult = _mapper.Map<IEnumerable<ComplaintViewModel>>(result);
                return ResponseCreater<IEnumerable<ComplaintViewModel>>.CreateSuccessResponse(mappedResult, "Complaints loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<IEnumerable<ComplaintViewModel>>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<ComplaintModel>> GetComplaintById(Guid id)
        {
            try
            {
                var result = await _complaintRepository.GetComplaintById(id);
                var mappedResult = _mapper.Map<ComplaintModel>(result);
                return ResponseCreater<ComplaintModel>.CreateSuccessResponse(mappedResult, "Complaint loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<ComplaintModel>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<ComplaintViewModel>> GetComplaintViewById(Guid id)
        {
            try
            {
                var result = await _complaintRepository.GetComplaintViewById(id);
                var mappedResult = _mapper.Map<ComplaintViewModel>(result);
                return ResponseCreater<ComplaintViewModel>.CreateSuccessResponse(mappedResult, "Complaint loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<ComplaintViewModel>.CreateErrorResponse(null, ex.ToString());
            }
        }


        public async Task<AppResponse<int>> AddModifyComplaint(ComplaintModel request)
        {
            try
            {
                // Changes needed for file manipulation
                var mappedRequest = _mapper.Map<Complaint>(request);
                if (request.File != null)
                {
                    // In case of Edit, delete prev file & add new one
                    if (mappedRequest.Id != Guid.Empty && !string.IsNullOrEmpty(mappedRequest.FileUrl))
                    {
                        await _fileUploader.Delete(mappedRequest.FileUrl, "complaints");
                    }
                    mappedRequest.FileUrl = await _fileUploader.Upload(request.File, "complaints");
                }

                if (request.StatusUpdateFile != null)
                {
                    // In case of Edit, delete prev file & add new one
                    if (mappedRequest.Id != Guid.Empty && !string.IsNullOrEmpty(mappedRequest.StatusUpdateFileUrl))
                    {
                        await _fileUploader.Delete(mappedRequest.StatusUpdateFileUrl, "complaints");
                    }
                    mappedRequest.StatusUpdateFileUrl = await _fileUploader.Upload(request.StatusUpdateFile, "complaints");
                }

                var result = await _complaintRepository.AddModifyComplaint(mappedRequest);
                return ResponseCreater<int>.CreateSuccessResponse(result, "Complaint add/update successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> DeleteComplaint(Guid id)
        {
            try
            {
                var imageUrl = await _complaintRepository.DeleteComplaint(id);
                // Need change to delete the image file as well
                if (imageUrl != null) await _fileUploader.Delete(imageUrl, "complaints");
                return ResponseCreater<int>.CreateSuccessResponse(1, "Complaint deleted successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

        public async Task<AppResponse<IEnumerable<ComplaintCommentViewModel>>> GetComplaintComments(Guid complaintId)
        {
            try
            {
                var result = await _complaintRepository.GetComplaintComments(complaintId);
                var mappedResult = _mapper.Map<IEnumerable<ComplaintCommentViewModel>>(result);
                return ResponseCreater<IEnumerable<ComplaintCommentViewModel>>.CreateSuccessResponse(mappedResult, "ComplaintComments loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<IEnumerable<ComplaintCommentViewModel>>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<ComplaintCommentModel>> GetComplaintCommentById(Guid id)
        {
            try
            {
                var result = await _complaintRepository.GetComplaintCommentById(id);
                var mappedResult = _mapper.Map<ComplaintCommentModel>(result);
                return ResponseCreater<ComplaintCommentModel>.CreateSuccessResponse(mappedResult, "ComplaintComment loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<ComplaintCommentModel>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> AddModifyComplaintComment(ComplaintCommentModel request)
        {
            try
            {
                // Changes needed for file manipulation
                var mappedRequest = _mapper.Map<ComplaintComment>(request);
                if (request.File != null)
                {
                    // In case of Edit, delete prev file & add new one
                    if (mappedRequest.Id != Guid.Empty && !string.IsNullOrEmpty(mappedRequest.FileUrl))
                    {
                        await _fileUploader.Delete(mappedRequest.FileUrl, "complaintcomments");
                    }
                    mappedRequest.FileUrl = await _fileUploader.Upload(request.File, "complaintcomments");
                }

                var result = await _complaintRepository.AddModifyComplaintComment(mappedRequest);
                return ResponseCreater<int>.CreateSuccessResponse(result, "ComplaintComment add/update successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> DeleteComplaintComment(Guid id)
        {
            try
            {
                var imageUrl = await _complaintRepository.DeleteComplaintComment(id);
                // Need change to delete the image file as well
                if (imageUrl != null) await _fileUploader.Delete(imageUrl, "complaintcomments");
                return ResponseCreater<int>.CreateSuccessResponse(1, "ComplaintComment deleted successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }
    }
}
