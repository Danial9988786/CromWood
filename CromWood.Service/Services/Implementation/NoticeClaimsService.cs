using AutoMapper;
using Azure.Core;
using CromWood.Business.Helper;
using CromWood.Business.Models;
using CromWood.Business.Models.ViewModel;
using CromWood.Business.Services.Interface;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;

namespace CromWood.Business.Services.Implementation
{
    public class NoticeClaimsService:INoticeClaimsService
    {
        private readonly INoticeClaimsRepository _noticeClaimRepository;
        private readonly IMapper _mapper;
        public NoticeClaimsService(INoticeClaimsRepository  noticeClaimsRepository, IMapper mapper) {
            _noticeClaimRepository = noticeClaimsRepository;
            _mapper = mapper;

        }
        public async Task<AppResponse<IEnumerable<NoticeViewModel>>> GetNotices(bool archived = false)
        {
            try
            {
                var result = await _noticeClaimRepository.GetNotices(archived);
                var mappedResult = _mapper.Map<IEnumerable<NoticeViewModel>>(result);
                return ResponseCreater<IEnumerable<NoticeViewModel>>.CreateSuccessResponse(mappedResult, "Notices loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<IEnumerable<NoticeViewModel>>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<NoticeModel>> GetNoticeById(Guid id)
        {
            try
            {
                var result = await _noticeClaimRepository.GetNoticeById(id);
                var mappedResult = _mapper.Map<NoticeModel>(result);
                return ResponseCreater<NoticeModel>.CreateSuccessResponse(mappedResult, "Notice loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<NoticeModel>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<NoticeViewModel>> GetNoticeViewById(Guid id)
        {
            try
            {
                var result = await _noticeClaimRepository.GetNoticeById(id);
                var mappedResult = _mapper.Map<NoticeViewModel>(result);
                return ResponseCreater<NoticeViewModel>.CreateSuccessResponse(mappedResult, "Notice view loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<NoticeViewModel>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> AddModifyNotice(NoticeModel request)
        {
            try
            {
                var mappedRequest = _mapper.Map<Notice>(request);
                var result = await _noticeClaimRepository.AddModifyNotice(mappedRequest);
                return ResponseCreater<int>.CreateSuccessResponse(result, "Notice add/update successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> DeleteNotice(Guid id)
        {
            try
            {
                var imageUrl = await _noticeClaimRepository.DeleteNotice(id);
                return ResponseCreater<int>.CreateSuccessResponse(1, "Notice deleted successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> ArchiveNotice(Guid id)
        {
            try
            {
                var imageUrl = await _noticeClaimRepository.ArchiveNotice(id);
                return ResponseCreater<int>.CreateSuccessResponse(1, "Notice archived successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

        public async Task<AppResponse<IEnumerable<ClaimViewModel>>> GetClaims()
        {
            try
            {
                var result = await _noticeClaimRepository.GetClaims();
                var mappedResult = _mapper.Map<IEnumerable<ClaimViewModel>>(result);
                return ResponseCreater<IEnumerable<ClaimViewModel>>.CreateSuccessResponse(mappedResult, "Claims loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<IEnumerable<ClaimViewModel>>.CreateErrorResponse(null, ex.ToString());
            }
        }
        public async Task<AppResponse<ClaimViewModel>> GetClaimViewById(Guid id)
        {
            try
            {
                var result = await _noticeClaimRepository.GetClaimById(id);
                var mappedResult = _mapper.Map<ClaimViewModel>(result);
                return ResponseCreater<ClaimViewModel>.CreateSuccessResponse(mappedResult, "Claim view loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<ClaimViewModel>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<ClaimModel>> GetClaimById(Guid id)
        {
            try
            {
                var result = await _noticeClaimRepository.GetClaimById(id);
                var mappedResult = _mapper.Map<ClaimModel>(result);
                return ResponseCreater<ClaimModel>.CreateSuccessResponse(mappedResult, "Claim loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<ClaimModel>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> AddModifyClaim(ClaimModel request)
        {
            try
            {
                var mappedRequest = _mapper.Map<Claim>(request);
                var result = await _noticeClaimRepository.AddModifyClaim(mappedRequest);
                return ResponseCreater<int>.CreateSuccessResponse(result, "Claim add/update successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> DeleteClaim(Guid id)
        {
            try
            {
                var imageUrl = await _noticeClaimRepository.DeleteClaim(id);
                return ResponseCreater<int>.CreateSuccessResponse(1, "Claim deleted successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }
    }
}
