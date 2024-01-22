using AutoMapper;
using CromWood.Business.Helper;
using CromWood.Business.Models;
using CromWood.Business.Models.ViewModel;
using CromWood.Business.Services.Interface;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;
using CromWood.Helper;

namespace CromWood.Business.Services.Implementation
{
    public class TenancyService : ITenancyService
    {
        private readonly ITenancyRepository _tenancyRepository;
        private readonly IMapper _mapper;
        private readonly IFileUploader _fileUploader;
        public TenancyService(ITenancyRepository tenancyRepository, IMapper mapper, IFileUploader fileUploader)
        {
            _tenancyRepository = tenancyRepository;
            _mapper = mapper;
            _fileUploader = fileUploader;
        }

        public async Task<AppResponse<IEnumerable<TenancyViewModel>>> GetTenancyForList()
        {
            try
            {
                var result = await _tenancyRepository.GetTenancyForList();
                var mappedResult = _mapper.Map<IEnumerable<TenancyViewModel>>(result);
                return ResponseCreater<IEnumerable<TenancyViewModel>>.CreateSuccessResponse(mappedResult, "Tenancies loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<IEnumerable<TenancyViewModel>>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<TenancyModel>> GetTenancyOverView(Guid tenancyId)
        {
            try
            {
                var result = await _tenancyRepository.GetTenancyOverView(tenancyId);
                var mappedResult = _mapper.Map<TenancyModel>(result);
                return ResponseCreater<TenancyModel>.CreateSuccessResponse(mappedResult, "Tenancy overview loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<TenancyModel>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<TenancyViewModel>> GetTenancyViewDetail(Guid tenancyId)
        {
            try
            {
                var result = await _tenancyRepository.GetTenancyViewDetail(tenancyId);
                var mappedResult = _mapper.Map<TenancyViewModel>(result);
                return ResponseCreater<TenancyViewModel>.CreateSuccessResponse(mappedResult, "Tenancy overview loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<TenancyViewModel>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> AddTenancy(TenancyModel tenancy)
        {
            try
            {
                var mappedTenancy = _mapper.Map<Tenancy>(tenancy);
                var result = await _tenancyRepository.AddTenancy(mappedTenancy);
                return ResponseCreater<int>.CreateSuccessResponse(result, "Tenancy added successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> EditTenancy(TenancyModel tenancy)
        {
            try
            {
                var mappedTenancy = _mapper.Map<Tenancy>(tenancy);
                var result = await _tenancyRepository.EditTenancy(mappedTenancy);
                return ResponseCreater<int>.CreateSuccessResponse(result, "Tenancy edited successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

        public async Task<AppResponse<IEnumerable<TenantViewModel>>> GetTenancyTenants(Guid tenancyId)
        {
            try
            {
                var result = await _tenancyRepository.GetTenancyTenants(tenancyId);
                var mappedResult = _mapper.Map<IEnumerable<TenantViewModel>>(result);
                return ResponseCreater<IEnumerable<TenantViewModel>>.CreateSuccessResponse(mappedResult, "Tenancies tenants loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<IEnumerable<TenantViewModel>>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> LinkTenancyTenant(TenancyTenantModel tenancyTenant)
        {
            try
            {
                var mappedTenancyTenant = _mapper.Map<TenancyTenant>(tenancyTenant);
                var result = await _tenancyRepository.LinkTenancyTenant(mappedTenancyTenant);
                return ResponseCreater<int>.CreateSuccessResponse(result, "Tenancy tenant linked successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

        public async Task<AppResponse<IEnumerable<TenancyNoteModel>>> GetTenancyNotes(Guid tenancyId)
        {
            try
            {
                var result = await _tenancyRepository.GetTenancyNotes(tenancyId);
                var mappedResult = _mapper.Map<IEnumerable<TenancyNoteModel>>(result);
                return ResponseCreater<IEnumerable<TenancyNoteModel>>.CreateSuccessResponse(mappedResult, "Tenancies notes loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<IEnumerable<TenancyNoteModel>>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<TenancyNoteModel>> GetTenancyNote(Guid id)
        {
            try
            {
                var result = await _tenancyRepository.GetTenancyNote(id);
                var mappedResult = _mapper.Map<TenancyNoteModel>(result);
                return ResponseCreater<TenancyNoteModel>.CreateSuccessResponse(mappedResult, "Note loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<TenancyNoteModel>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> AddModifyNote(TenancyNoteModel note)
        {
            try
            {
                var mappedNote = _mapper.Map<TenancyNote>(note);
                var result = 0;
                if (note.File != null)
                {
                    // In case of Edit, delete prev file & add new one
                    if (mappedNote.Id != Guid.Empty)
                    {
                        await _fileUploader.Delete(mappedNote.FileUrl, "tenancynote");
                    }
                    mappedNote.FileUrl = await _fileUploader.Upload(note.File, "tenancynote");
                }

                result = mappedNote.Id == Guid.Empty ? await _tenancyRepository.AddNote(mappedNote) : await _tenancyRepository.ModifyNote(mappedNote);
                return ResponseCreater<int>.CreateSuccessResponse(result, "Note add/update successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> DeleteNote(Guid id)
        {
            try
            {
                var imageUrl = await _tenancyRepository.DeleteNote(id);
                if (imageUrl != null) await _fileUploader.Delete(imageUrl, "tenancynote");
                return ResponseCreater<int>.CreateSuccessResponse(1, "Note deleted successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

        public async Task<AppResponse<IEnumerable<TenancyDocumentModel>>> GetTenancyDocuments(Guid tenancyId)
        {
            try
            {
                var result = await _tenancyRepository.GetTenancyDocuments(tenancyId);
                var mappedResult = _mapper.Map<IEnumerable<TenancyDocumentModel>>(result);
                return ResponseCreater<IEnumerable<TenancyDocumentModel>>.CreateSuccessResponse(mappedResult, "Tenancies documents loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<IEnumerable<TenancyDocumentModel>>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<TenancyDocumentModel>> GetTenancyDocument(Guid id)
        {
            try
            {
                var result = await _tenancyRepository.GetTenancyDocument(id);
                var mappedResult = _mapper.Map<TenancyDocumentModel>(result);
                return ResponseCreater<TenancyDocumentModel>.CreateSuccessResponse(mappedResult, "Document loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<TenancyDocumentModel>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> AddModifyDocument(TenancyDocumentModel document)
        {
            try
            {
                var mappedDocument = _mapper.Map<TenancyDocument>(document);
                var result = 0;
                if (document.File != null)
                {
                    // In case of Edit, delete prev file & add new one
                    if (mappedDocument.Id != Guid.Empty)
                    {
                        await _fileUploader.Delete(mappedDocument.DocUrl, "tenancydocument");
                    }
                    mappedDocument.DocUrl = await _fileUploader.Upload(document.File, "tenancydocument");
                }

                result = mappedDocument.Id == Guid.Empty ? await _tenancyRepository.AddDocument(mappedDocument) : await _tenancyRepository.ModifyDocument(mappedDocument);
                return ResponseCreater<int>.CreateSuccessResponse(result, "Document add/update successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> DeleteDocument(Guid id)
        {
            try
            {
                var imageUrl = await _tenancyRepository.DeleteDocument(id);
                if (imageUrl != null) await _fileUploader.Delete(imageUrl, "tenancydocument");
                return ResponseCreater<int>.CreateSuccessResponse(1, "Document deleted successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

        public async Task<AppResponse<ICollection<NoticeViewModel>>> GetNoticesForTenancyTenants(Guid tenancyId)
        {
            try
            {
                var result = await _tenancyRepository.GetNoticesForTenancyTenants(tenancyId);
                var mappedResult = _mapper.Map<ICollection<NoticeViewModel>>(result);
                return ResponseCreater<ICollection<NoticeViewModel>>.CreateSuccessResponse(mappedResult, "Tenancy Notices loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<ICollection<NoticeViewModel>>.CreateErrorResponse(null, ex.ToString());
            }
        }
        public async Task<AppResponse<ICollection<ComplaintViewModel>>> GetComplaintsForTenancyTenants(Guid tenancyId)
        {
            try
            {
                var result = await _tenancyRepository.GetComplaintsForTenancyTenants(tenancyId);
                var mappedResult = _mapper.Map<ICollection<ComplaintViewModel>>(result);
                return ResponseCreater<ICollection<ComplaintViewModel>>.CreateSuccessResponse(mappedResult, "Tenancy Complaints loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<ICollection<ComplaintViewModel>>.CreateErrorResponse(null, ex.ToString());
            }
        }

    }
}
