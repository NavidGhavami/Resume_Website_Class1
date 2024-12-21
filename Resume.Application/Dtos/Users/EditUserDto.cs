namespace Resume.Application.Dtos.Users
{
    public class EditUserDto : CreateUserDto
    {
        public long Id { get; set; }
    }

    public enum EditUserResult
    {
        Success,
        UserNotFound,
        Error
    }
}
