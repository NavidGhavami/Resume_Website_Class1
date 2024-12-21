namespace Resume.Application.Dtos.Users
{
    public class UserDetailDto
    {
        #region Properties

        public string Fullname { get; set; }
        public string? Email { get; set; }
        public string Mobile { get; set; }
        public string BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Description { get; set; }
        public bool IsBlock { get; set; }
        public string? Avatar { get; set; }

        #endregion
    }
}
