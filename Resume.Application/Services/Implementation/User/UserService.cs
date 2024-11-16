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




    #region Dispose

    public async ValueTask DisposeAsync()
    {
        // TODO release managed resources here
    }

    #endregion
}