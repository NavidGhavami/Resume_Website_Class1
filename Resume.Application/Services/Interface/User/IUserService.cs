using Resume.Application.Dtos.Users;

namespace Resume.Application.Services.Interface.User;

public interface IUserService : IAsyncDisposable
{
    //Task<FilterUserDto> GetUsers();

    Task<bool> IsMobileExist(string mobile);
    Task<CreateUserResult> CreateUser(CreateUserDto user);
    Task<List<FilterUserDto>> GetAllUsers(FilterUserDto filter);
    Task<UserDetailDto> GetUserDetail();
    Task<EditUserDto> GetForEdit(long id);
    Task<EditUserResult> EditUser(EditUserDto command);
    Task<bool> BlockUser(long id);
    Task<bool> UnBlockUser(long id);

}