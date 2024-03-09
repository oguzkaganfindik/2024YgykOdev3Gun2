using Courses.Business.Dtos;
using Courses.Business.Types;

namespace Courses.Business.Services
{
    public interface IUserService
    {
        ServiceMessage AddUser(UserAddDto userAddDto);
        UserInfoDto LoginUser(UserLoginDto userLoginDto);

    }
}
