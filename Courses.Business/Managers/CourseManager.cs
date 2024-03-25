using Courses.Business.Dtos;
using Courses.Business.Services;
using Courses.Data.Entities;
using Courses.Data.Repositories;

namespace Courses.Business.Managers
{
    public class CourseManager : ICourseService
    {
        private readonly IRepository<CourseEntity> _courseRepository;
        private readonly IRepository<InstructorEntity> _instructorRepository;
        private readonly IRepository<CategoryEntity> _categoryRepository;

        public CourseManager(IRepository<CourseEntity> courseRepository, IRepository<InstructorEntity> instructorRepository, IRepository<CategoryEntity> categoryRepository)
        {
            _courseRepository = courseRepository;
            _instructorRepository = instructorRepository;
            _categoryRepository = categoryRepository;
        }

        public bool AddCourse(CourseAddDto courseAddDto)
        {
            var hasCourse = _courseRepository.GetAll(x => x.Name.ToLower() == courseAddDto.Name.ToLower()).ToList();

            if (hasCourse.Any())
            {
                return false;
            }

            var entity = new CourseEntity()
            {
                Name = courseAddDto.Name,
                Description = courseAddDto.Description,
                UnitPrice = courseAddDto.UnitPrice,
                UnitsInStock = courseAddDto.UnitsInStock,
                ImagePath = courseAddDto.ImagePath,
                CategoryId = courseAddDto.CategoryId,
                InstructorId = courseAddDto.InstructorId,
            };

            _courseRepository.Add(entity);
            return true;
        }

        public bool DeleteCourse(int id)
        {
            var firstCourse = _courseRepository.Get(x => x.CategoryId == id);

            if (firstCourse is not null)
            {
                return false;
            }

            _courseRepository.Delete(id);
            return true;
        }

        public List<CourseListDto> GetCourses()
        {
            var courseEntities = _courseRepository.GetAll().OrderBy(x => x.Name);
            var courseDtoList = courseEntities.Select(x => new CourseListDto()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                UnitPrice = x.UnitPrice,
                UnitsInStock = x.UnitsInStock,
                ImagePath = x.ImagePath,
                CategoryName = x.Category.Name,
                InstructorName = x.Instructor.Name,
            }).ToList();

            return courseDtoList;
        }

        public CourseUpdateDto GetCourse(int id)
        {
            var entity = _courseRepository.GetById(id);

            var courseUpdateDto = new CourseUpdateDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                UnitPrice = entity.UnitPrice,
                UnitsInStock = entity.UnitsInStock,
                ImagePath = entity.ImagePath,
                CategoryId = entity.CategoryId,
                InstructorId = entity.InstructorId,
            };

            return courseUpdateDto;
        }

        public bool UpdateCourse(CourseUpdateDto courseUpdateDto)
        {
            var hasCourse = _courseRepository.GetAll(x => x.Name.ToLower() == courseUpdateDto.Name.ToLower() && x.Id != courseUpdateDto.Id).ToList();

            if (hasCourse.Any())
            {
                return false;
            }

            var entity = _courseRepository.GetById(courseUpdateDto.Id);

            entity.Name = courseUpdateDto.Name;
            entity.Description = courseUpdateDto.Description;
            entity.UnitPrice = entity.UnitPrice;
            entity.UnitsInStock = entity.UnitsInStock;
            entity.ImagePath = entity.ImagePath;
            entity.CategoryId = entity.CategoryId;
            entity.InstructorId = entity.InstructorId;

            _courseRepository.Update(entity);

            return true;
        }
    }
}
