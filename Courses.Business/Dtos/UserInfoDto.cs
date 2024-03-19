using Courses.Data.Enums;

namespace Courses.Business.Dtos
{
    public class UserInfoDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public UserTypeEnum UserType { get; set; }
    }
}
