using Courses.Business.Dtos;
using Courses.Business.Services;
using Courses.Data.Entities;
using Courses.Data.Repositories;

namespace Courses.Business.Managers
{
    public class CourseManager : ICourseService
    {
        private readonly IRepository<CourseEntity> _courseRepository;

        public CourseManager(IRepository<CourseEntity> courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public void AddCourse(CourseAddDto courseAddDto)
        {
            var entity = new CourseEntity()
            {
                Name = courseAddDto.Name,
                Description = courseAddDto.Description,
                UnitPrice = courseAddDto.UnitPrice,
                UnitsInStock = courseAddDto.UnitsInStock,
                CategoryId = courseAddDto.CategoryId,
                ImagePath = courseAddDto.ImagePath,
            };

            _courseRepository.Add(entity);
        }

        public void DeleteCourse(int id)
        {
            _courseRepository.Delete(id);
        }

        public CourseUpdateDto GetCourseById(int id)
        {
            var entity = _courseRepository.GetById(id);

            var courseUpdateDto = new CourseUpdateDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                UnitPrice = entity.UnitPrice,
                UnitsInStock = entity.UnitsInStock,
                CategoryId = entity.CategoryId,
                ImagePath = entity.ImagePath,
            };

            return courseUpdateDto;
        }

        public List<CourseListDto> GetCourses()
        {
            var courseEntites = _courseRepository.GetAll().OrderBy(x => x.Category.Name).ThenBy(x => x.Name);


            var courseDtoList = courseEntites.Select(x => new CourseListDto()
            {
                Id = x.Id,
                Name = x.Name,
                UnitPrice = x.UnitPrice,
                UnitsInStock = x.UnitsInStock,
                CategoryName = x.Category.Name,
                ImagePath = x.ImagePath,
            }).ToList();

            return courseDtoList;
        }

        public List<CourseListDto> GetCoursesByCategoryId(int? categoryId = null)
        {
            if (categoryId.HasValue) 
            {
                var courseEntities = _courseRepository.GetAll(x => x.CategoryId == categoryId).OrderBy(x => x.Name);


                var courseDtos = courseEntities.Select(x => new CourseListDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    UnitPrice = x.UnitPrice,
                    UnitsInStock = x.UnitsInStock,
                    CategoryName = x.Category.Name,
                    ImagePath = x.ImagePath
                }).ToList();

                return courseDtos;
            }

            return GetCourses(); 


        }

        public void UpdateCourse(CourseUpdateDto courseUpdateDto)
        {
            var entity = _courseRepository.GetById(courseUpdateDto.Id);

            entity.Name = courseUpdateDto.Name;
            entity.Description = courseUpdateDto.Description;
            entity.UnitPrice = courseUpdateDto.UnitPrice;
            entity.UnitsInStock = courseUpdateDto.UnitsInStock;
            entity.CategoryId = courseUpdateDto.CategoryId;

            if(courseUpdateDto.ImagePath != "")
            entity.ImagePath = courseUpdateDto.ImagePath;

            _courseRepository.Update(entity);
        }
    }
}