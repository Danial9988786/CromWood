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

        public async Task<AppResponse<ICollection<TenancyMessageViewModel>>> GetTenancyMessages(Guid id)
        {
            try
            {
                var result = await _tenancyRepository.GetTenancyMessages(id);
                var mappedResult = _mapper.Map<ICollection<TenancyMessageViewModel>>(result);
                return ResponseCreater<ICollection<TenancyMessageViewModel>>.CreateSuccessResponse(mappedResult, "Tenancies messages loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<ICollection<TenancyMessageViewModel>>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<TenancyMessageModel>> GetTenancyMessage(Guid messageId)
        {
            try
            {
                var result = await _tenancyRepository.GetTenancyMessage(messageId);
                var mappedResult = _mapper.Map<TenancyMessageModel>(result);
                return ResponseCreater<TenancyMessageModel>.CreateSuccessResponse(mappedResult, "Message loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<TenancyMessageModel>.CreateErrorResponse(null, ex.ToString());
            }
        }
        public async Task<AppResponse<int>> AddModifyMessage(TenancyMessageModel message)
        {
            try
            {
                var mappedMessage = _mapper.Map<TenancyMessage>(message);
                var result = 0;
                if (message.Attachment != null)
                {
                    // In case of Edit, delete prev file & add new one
                    if (mappedMessage.Id != Guid.Empty)
                    {
                        await _fileUploader.Delete(mappedMessage.AttachmentUrl, "tenancymessage");
                    }
                    mappedMessage.AttachmentUrl = await _fileUploader.Upload(message.Attachment, "tenancymessage");
                }

                result = await _tenancyRepository.AddModifyMessage(mappedMessage);
                return ResponseCreater<int>.CreateSuccessResponse(result, "Note add/update successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> DeleteMessage(Guid messageId)
        {
            try
            {
                var attachmentUrl = await _tenancyRepository.DeleteMessage(messageId);
                if (attachmentUrl != null) await _fileUploader.Delete(attachmentUrl, "tenancymessage");
                return ResponseCreater<int>.CreateSuccessResponse(1, "Message deleted successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

        public async Task<AppResponse<ICollection<UnitUtilityViewModel>>> GetUnitUtilities(Guid id)
        {
            try
            {
                var result = await _tenancyRepository.GetUnitUtilities(id);
                var mappedResult = _mapper.Map<ICollection<UnitUtilityViewModel>>(result);
                return ResponseCreater<ICollection<UnitUtilityViewModel>>.CreateSuccessResponse(mappedResult, "Tenancies utilities loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<ICollection<UnitUtilityViewModel>>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<UnitUtilityModel>> GetUnitUtility(Guid id)
        {
            try
            {
                var result = await _tenancyRepository.GetUnitUtility(id);
                var mappedResult = _mapper.Map<UnitUtilityModel>(result);
                return ResponseCreater<UnitUtilityModel>.CreateSuccessResponse(mappedResult, "Utility loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<UnitUtilityModel>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<UnitUtilityViewModel>> GetUnitUtilityView(Guid id)
        {
            try
            {
                var result = await _tenancyRepository.GetUnitUtilityView(id);
                var mappedResult = _mapper.Map<UnitUtilityViewModel>(result);
                return ResponseCreater<UnitUtilityViewModel>.CreateSuccessResponse(mappedResult, "Utility loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<UnitUtilityViewModel>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> AddModifyUnitUtility(UnitUtilityModel req)
        {
            try
            {
                var mapped = _mapper.Map<UnitUtility>(req);
                var result = await _tenancyRepository.AddModifyUnitUtility(mapped);
                return ResponseCreater<int>.CreateSuccessResponse(result, "Utility add/update successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> DeleteUnitUtility(Guid id)
        {
            try
            {
                var result = await _tenancyRepository.DeleteUnitUtility(id);
                return ResponseCreater<int>.CreateSuccessResponse(1, "Utility deleted successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

        public async Task<AppResponse<UnitUtilityReadingModel>> GetUnitUtilityReading(Guid id)
        {
            try
            {
                var result = await _tenancyRepository.GetUnitUtilityReading(id);
                var mappedResult = _mapper.Map<UnitUtilityReadingModel>(result);
                return ResponseCreater<UnitUtilityReadingModel>.CreateSuccessResponse(mappedResult, "Utility reading loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<UnitUtilityReadingModel>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> AddModifyUnitUtilityReading(UnitUtilityReadingModel req)
        {
            try
            {
                var mappedMessage = _mapper.Map<UnitUtilityReading>(req);
                var result = await _tenancyRepository.AddModifyUnitUtilityReading(mappedMessage);
                return ResponseCreater<int>.CreateSuccessResponse(result, "Utility reading add/update successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> DeleteUnitUtilityReading(Guid id)
        {
            try
            {
                var result = await _tenancyRepository.DeleteUnitUtilityReading(id);
                return ResponseCreater<int>.CreateSuccessResponse(result, "Utility reading deleted successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

        public async Task<AppResponse<UnitUtilityDocumentModel>> GetUnitUtilityDocument(Guid id)
        {
            try
            {
                var result = await _tenancyRepository.GetUnitUtilityDocument(id);
                var mappedResult = _mapper.Map<UnitUtilityDocumentModel>(result);
                return ResponseCreater<UnitUtilityDocumentModel>.CreateSuccessResponse(mappedResult, "Utility document loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<UnitUtilityDocumentModel>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> AddModifyUnitUtilityDocument(UnitUtilityDocumentModel req)
        {
            try
            {
                var mappedReq = _mapper.Map<UnitUtilityDocument>(req);
                var result = 0;
                if (req.Document != null)
                {
                    // In case of Edit, delete prev file & add new one
                    if (mappedReq.Id != Guid.Empty)
                    {
                        await _fileUploader.Delete(mappedReq.DocUrl, "tenancyutility");
                    }
                    mappedReq.DocUrl = await _fileUploader.Upload(req.Document, "tenancyutility");
                }

                result = await _tenancyRepository.AddModifyUnitUtilityDocument(mappedReq);
                return ResponseCreater<int>.CreateSuccessResponse(result, "Utility document add/update successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> DeleteUnitUtilityDocument(Guid id)
        {
            try
            {
                var docUrl = await _tenancyRepository.DeleteUnitUtilityDocument(id);
                if (docUrl != null) await _fileUploader.Delete(docUrl, "tenancyutility");
                return ResponseCreater<int>.CreateSuccessResponse(1, "Utility document deleted successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

    }
}
