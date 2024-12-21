using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Resume.Application.Dtos.Users;
using Resume.Application.Services.Interface.PasswordHasher;
using Resume.Application.Services.Interface.User;
using Resume.Application.Utilities;
using Resume.Domain.Repository;


namespace Resume.Application.Services.Implementation.User;

public class UserService : IUserService
{
	#region Constructor

	private readonly IGenericRepository<Domain.Entities.User.User> _userRepository;
	private readonly IPasswordHasher _passwordHasher;

	public UserService(IGenericRepository<Domain.Entities.User.User> userRepository, IPasswordHasher passwordHasher)
	{
		_userRepository = userRepository;
		_passwordHasher = passwordHasher;
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
			// var hashPassword = _passwordHasher.EncodePasswordMd5(user.Password);
			var hashPassword = Utilities.PasswordHasher.HashPasswordSHA256(user.Password);
			var newUser = new Domain.Entities.User.User
			{
				Fullname = user.Fullname,
				Email = user.Email,
				Mobile = user.Mobile,
				BirthDate = user.BirthDate,
				BirthPlace = user.BirthPlace,
				Description = user.Description,
				Password = hashPassword,
				ConfirmPassword = hashPassword,
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

	#region Filter Users

	public async Task<List<FilterUserDto>> GetAllUsers(FilterUserDto filter)
	{
		var query = _userRepository.GetQuery().AsQueryable();

		if (!string.IsNullOrWhiteSpace(filter.Fullname))
		{
			query = query.Where(x => EF.Functions.Like(x.Fullname, $"%{filter.Fullname}%"));
			//query = query.Where(x => x.Fullname.Contains(filter.Fullname));
		}

		if (!string.IsNullOrWhiteSpace(filter.Mobile))
		{
			query = query.Where(x => EF.Functions.Like(x.Mobile, $"%{filter.Mobile}%"));
		}

		var allUsers = await query.Select(x => new FilterUserDto
		{
			Id = x.Id,
			Fullname = x.Fullname,
			Email = x.Email,
			Mobile = x.Mobile,
			IsBlock = x.IsBlock,
			Avatar = x.Avatar,
			CreateDate = x.CreateDate.ToStringShamsiDate(),
		}).OrderByDescending(x => x.Id).ToListAsync();

		return allUsers;

	}

	public async Task<UserDetailDto> GetUserDetail()
	{
		var user = await _userRepository
			.GetQuery()
			.AsQueryable()
			.FirstOrDefaultAsync(x => !x.IsDelete);

		return new UserDetailDto
		{
			Fullname = user.Fullname,
			Email = user.Email,
			Mobile = user.Mobile,
			IsBlock = user.IsBlock,
			Avatar = user.Avatar,
			BirthDate = user.BirthDate,
			BirthPlace = user.BirthPlace,
			Description = user.Description
		};

	}

	#endregion

	#region Edit User

	public async Task<EditUserDto> GetForEdit(long id)
	{
		var user = await _userRepository
			.GetQuery()
			.AsQueryable()
			.FirstOrDefaultAsync(x => x.Id == id);

		if (user == null)
		{
			return new EditUserDto();
		}

		return new EditUserDto
		{
			Id = id,
			Fullname = user.Fullname,
			Email = user.Email,
			Mobile = user.Mobile,
			Password = user.Password,
			ConfirmPassword = user.ConfirmPassword,
			BirthDate = user.BirthDate,
			BirthPlace = user.BirthPlace,
			Description = user.Description,
			IsBlock = user.IsBlock,
			Avatar = user.Avatar,
		};
	}

	public async Task<EditUserResult> EditUser(EditUserDto command)
	{
		var user = await _userRepository
			.GetQuery()
			.AsQueryable()
			.FirstOrDefaultAsync(x => x.Id == command.Id);

		//var hashPassword = _passwordHasher.EncodePasswordMd5(command.Password);
		var hashPassword = Utilities.PasswordHasher.HashPasswordSHA256(user.Password);


		if (user == null)
		{
			return EditUserResult.UserNotFound;
		}

		user.Fullname = command.Fullname;
		user.Email = command.Email;
		user.Mobile = command.Mobile;
		user.Password = hashPassword;
		user.ConfirmPassword = hashPassword;
		user.BirthDate = command.BirthDate;
		user.BirthPlace = command.BirthPlace;
		user.Description = command.Description;
		user.IsBlock = command.IsBlock;
		user.Avatar = command.Avatar;
		user.UpdateDate = DateTime.Now;

		_userRepository.EditEntity(user);
		await _userRepository.SaveChanges();


		return EditUserResult.Success;
	}



	#endregion

	#region Block and UnBlock User

	public async Task<bool> BlockUser(long id)
	{
		var user = await _userRepository
			.GetQuery()
			.AsQueryable()
			.FirstOrDefaultAsync(x => x.Id == id);

		if (user == null)
		{
			return false;
		}

		user.IsBlock = true;

		_userRepository.EditEntity(user);
		await _userRepository.SaveChanges();

		return true;
	}

	public async Task<bool> UnBlockUser(long id)
	{
		var user = await _userRepository
			.GetQuery()
			.AsQueryable()
			.FirstOrDefaultAsync(x => x.Id == id);

		if (user == null)
		{
			return false;
		}

		user.IsBlock = false;

		_userRepository.EditEntity(user);
		await _userRepository.SaveChanges();

		return true;
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