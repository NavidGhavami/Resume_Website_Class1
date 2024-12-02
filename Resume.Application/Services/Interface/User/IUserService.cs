using Resume.Application.Dtos.Users;

namespace Resume.Application.Services.Interface.User;

public interface IUserService : IAsyncDisposable
{
    //Task<FilterUserDto> GetUsers();

    Task<bool> IsMobileExist(string mobile);
    Task<CreateUserResult> CreateUser(CreateUserDto user);

}