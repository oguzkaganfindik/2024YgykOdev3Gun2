using Courses.Business.Dtos;
using Courses.Business.Services;
using Courses.Data.Entities;
using Courses.Data.Repositories;

namespace Courses.Business.Managers
{
    public class InstructorManager : IInstructorService
    {
        private readonly IRepository<InstructorEntity> _instructorRepository;
        private readonly IRepository<CourseEntity> _courseRepository;

        public InstructorManager(IRepository<InstructorEntity> instructorRepository, IRepository<CourseEntity> courseRepository)
        {
            _instructorRepository = instructorRepository;
            _courseRepository = courseRepository;
        }

        public bool AddInstructor(InstructorAddDto instructorAddDto)
        {
            var hasInstructor = _instructorRepository.GetAll(x => x.FirstName.ToLower() == instructorAddDto.FirstName.ToLower()).ToList();

            if (hasInstructor.Any())
            {
                return false;
            }

            var entity = new InstructorEntity()
            {
                FirstName = instructorAddDto.FirstName,
                LastName = instructorAddDto.LastName,
                Email = instructorAddDto.Email,
                Password = instructorAddDto.Password,
                
            };

            _instructorRepository.Add(entity);
            return true;
        }

        public bool DeleteInstructor(int id)
        {
            var firstCourse = _courseRepository.Get(x => x.InstructorId == id);

            if (firstCourse is not null)
            {
                return false;
            }

            _instructorRepository.Delete(id);
            return true;
        }

        public List<InstructorListDto> GetInstructors()
        {
            var instructorEntities = _instructorRepository.GetAll().OrderBy(x => x.FirstName);
            var instructorDtoList = instructorEntities.Select(x => new InstructorListDto()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Password = x.Password,

            }).ToList();

            return instructorDtoList;
        }

        public InstructorUpdateDto GetInstructor(int id)
        {
            var entity = _instructorRepository.GetById(id);

            var instructorUpdateDto = new InstructorUpdateDto()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Password = entity.Password,
            };

            return instructorUpdateDto;
        }

        public bool UpdateInstructor(InstructorUpdateDto instructorUpdateDto)
        {
            var hasInstructor = _instructorRepository.GetAll(x => x.FirstName.ToLower() == instructorUpdateDto.FirstName.ToLower() && x.Id != instructorUpdateDto.Id).ToList();

            if (hasInstructor.Any())
            {
                return false;
            }

            var entity = _instructorRepository.GetById(instructorUpdateDto.Id);

            entity.FirstName = instructorUpdateDto.FirstName;
            entity.LastName = instructorUpdateDto.LastName;
            entity.Email = instructorUpdateDto.Email;
            entity.Password = instructorUpdateDto.Password;


            _instructorRepository.Update(entity);

            return true;
        }
    }
}
