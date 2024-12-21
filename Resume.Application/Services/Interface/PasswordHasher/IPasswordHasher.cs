namespace Resume.Application.Services.Interface.PasswordHasher
{
    public interface IPasswordHasher
    {
        string EncodePasswordMd5(string pass);
    }
}
