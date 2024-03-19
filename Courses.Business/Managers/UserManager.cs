using Courses.Business.Dtos;
using Courses.Business.Services;
using Courses.Business.Types;
using Courses.Data.Entities;
using Courses.Data.Enums;
using Courses.Data.Repositories;
using Microsoft.AspNetCore.DataProtection;

namespace Courses.Business.Managers
{
    public class UserManager : IUserService
    {
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IDataProtector _dataProtector;
        public UserManager(IRepository<UserEntity> userRepository, IDataProtectionProvider dataProtectionProvider)
        {
            _userRepository = userRepository;
            _dataProtector = dataProtectionProvider.CreateProtector("security");
        }
        public ServiceMessage AddUser(UserAddDto userAddDto)
        {
            var hasMail = _userRepository.GetAll(x => x.Email.ToLower() == userAddDto.Email.ToLower()).ToList();

            if (hasMail.Any()) 
            {
                return new ServiceMessage()
                {
                    IsSucceed = false,
                    Message = "Bu Eposta adresli bir kullanıcı zaten mevcut."
                };
            }

            var userEntity = new UserEntity()
            {
                FirstName = userAddDto.FirstName,
                LastName = userAddDto.LastName,
                Email = userAddDto.Email,
                Password = _dataProtector.Protect(userAddDto.Password),
                UserType = UserTypeEnum.User
            };

            _userRepository.Add(userEntity);

            return new ServiceMessage()
            {
                IsSucceed = true,
                Message = "Kayıt başarıyla tamamlandı."
            };
        }

        public UserInfoDto LoginUser(UserLoginDto userLoginDto)
        {
            var userEntity = _userRepository.Get(x => x.Email == userLoginDto.Email);

            if (userEntity is null)
            {
                return null;
            }

            var rawPassword = _dataProtector.Unprotect(userEntity.Password); 

            if (rawPassword == userLoginDto.Password)
            {
                return new UserInfoDto()
                {
                    Id = userEntity.Id,
                    Email = userEntity.Email,
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    UserType = userEntity.UserType
                };
            }
            else
            {
                return null;
            }
        }
    }
}
