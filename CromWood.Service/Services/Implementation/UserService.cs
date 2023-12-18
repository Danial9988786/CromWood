using AutoMapper;
using CromWood.Business.Helper;
using CromWood.Business.Models;
using CromWood.Business.Services.Interface;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;

namespace CromWood.Business.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }
        public async Task<AppResponse<IEnumerable<UserViewModel>>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userRepo.GetAllUsers();
                var mapppedUsers = _mapper.Map<IEnumerable<UserViewModel>>(users);
                return ResponseCreater<IEnumerable<UserViewModel>>.CreateSuccessResponse(mapppedUsers, "Users loaded successfully");

            }
            catch (Exception ex)
            {
                return ResponseCreater<IEnumerable<UserViewModel>>.CreateErrorResponse(null, ex.ToString());
            }
        }
        public async Task<AppResponse<string>> InviteUser(UserModel user)
        {
            try
            {
                
                var mappedUser = _mapper.Map<User>(user);
                var result = await _userRepo.InviteUser(mappedUser);
                if (result == 1)
                {
                    // Sending Email to user about their email and default password.
                }
                return ResponseCreater<string>.CreateSuccessResponse(null, "User invited successfully");

            }
            catch (Exception ex)
            {
                return ResponseCreater<string>.CreateErrorResponse(null, ex.ToString());
            }
        }
        public async Task<AppResponse<string>> ChangeUserRole(Guid UserId, Guid RoleId)
        {
            try
            {
                await _userRepo.ChangeUserRole(UserId, RoleId);
                return ResponseCreater<string>.CreateSuccessResponse(null, "User role changed successfully");

            }
            catch (Exception ex)
            {
                return ResponseCreater<string>.CreateErrorResponse(null, ex.ToString());
            }
        }
        public async Task<AppResponse<string>> BlockUserById(Guid Id)
        {
            try
            {
                await _userRepo.BlockUserById(Id);
                return ResponseCreater<string>.CreateSuccessResponse(null, "User blocked successfully");
            }
            catch (Exception ex)
            {
                return ResponseCreater<string>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<string>> DeleteUserById(Guid Id)
        {
            try
            {
                await _userRepo.DeleteUserById(Id);
                return ResponseCreater<string>.CreateSuccessResponse(null, "User deleted Successfully");

            }
            catch (Exception ex)
            {
                return ResponseCreater<string>.CreateErrorResponse(null, ex.ToString());
            }
        }
    }
}
