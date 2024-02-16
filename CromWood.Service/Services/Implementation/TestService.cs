using AutoMapper;
using CromWood.Business.Helper;
using CromWood.Business.Models;
using CromWood.Business.Models.ViewModel;
using CromWood.Business.Services.Interface;
using CromWood.Business.ViewModels;
using CromWood.Data.Repository.Interface;
using System.Collections.Generic;

namespace CromWood.Business.Services.Implementation
{
    public class TestService : ITestService
    {
        private readonly ITestRepository testRepository;
        private readonly IMapper _mapper;
        private readonly IComplaintRepository _complaintRepo;
        private readonly ITenancyRepository _tenencyRepo;
        public TestService(ITestRepository repository, IMapper mapper, IComplaintRepository complaintRepo, ITenancyRepository tenancyRepository)
        {
            testRepository = repository;
            _mapper = mapper;
            _complaintRepo = complaintRepo;
            _tenencyRepo = tenancyRepository;
        }
        public async Task<IEnumerable<TestModel>> GetTests()
        {
            var testViews = new List<TestModel>();
            var tests = await testRepository.GetModifiedTests();
            tests.ToList().ForEach((test) =>
            {
                testViews.Add(new TestModel()
                {
                    Name = test.Name,
                });
            });
            return testViews;
        }

        public async Task<AppResponse<DashboardModel>> GetDashboardOverview()
        {
            try
            {
                var complaints = await _complaintRepo.GetComplaints();
                var tenancy = await _tenencyRepo.GetTenancyForList();
                var dashboardOverview = new DashboardModel()
                {
                    RecentComplaints = _mapper.Map<List<ComplaintViewModel>>(complaints),
                    Tenancies = _mapper.Map<List<TenancyViewModel>>(tenancy)
                };
                return ResponseCreater<DashboardModel>.CreateSuccessResponse(dashboardOverview, "Dashboard overview successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<DashboardModel>.CreateErrorResponse(new DashboardModel() { }, ex.ToString());
            }
        }

        public async Task<AppResponse<DashboardModel>> GetDashboardOverviewJSON()
        {
            try
            {
                var result = await testRepository.GetDashboardOverviewJSON();
                var mapped = _mapper.Map<DashboardModel>(result);
                return ResponseCreater<DashboardModel>.CreateSuccessResponse(mapped, "Dashboard overview JSON successfully loaded");
            }

            catch (Exception ex)
            {
                return ResponseCreater<DashboardModel>.CreateErrorResponse(new DashboardModel() { }, ex.ToString());
            }
        }
    }
}
