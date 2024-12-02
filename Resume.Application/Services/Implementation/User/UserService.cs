using Microsoft.EntityFrameworkCore;
using Resume.Application.Dtos.Users;
using Resume.Application.Services.Interface.User;
using Resume.Domain.Repository;


namespace Resume.Application.Services.Implementation.User;

public class UserService : IUserService
{
	#region Constructor

	private readonly IGenericRepository<Domain.Entities.User.User> _userRepository;

	public UserService(IGenericRepository<Domain.Entities.User.User> userRepository)
	{
		_userRepository = userRepository;
	}

    #endregion

    #region User

    #region Is Mobile Exist

    public async Task<bool> IsMobileExist(string mobile)
    {
        return await _userRepository.GetQuery().AsQueryable().AnyAsync(x => x.Mobile == mobile);
    }

    #endregion

    #region Create User

    public async Task<CreateUserResult> CreateUser(CreateUserDto user)
    {
        if (!await IsMobileExist(user.Mobile))
        {
            var newUser = new Domain.Entities.User.User
            {
                Fullname = user.Fullname,
                Email = user.Email,
                Mobile = user.Mobile,
                Password = user.Password,
                ConfirmPassword = user.ConfirmPassword,
                IsBlock = user.IsBlock,
                Avatar = null, 
            };

            await _userRepository.AddEntity(newUser);
            await _userRepository.SaveChanges();

            return CreateUserResult.Success;
        }
        else
        {
            return CreateUserResult.DuplicateMobile;
        }
    }

    #endregion

    #endregion



    #region Dispose

    public async ValueTask DisposeAsync()
    {
        // TODO release managed resources here
    }

    #endregion

    
}