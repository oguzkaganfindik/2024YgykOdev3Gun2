using Courses.Business.Dtos;
using Courses.Business.Services;
using Courses.Data.Entities;
using Courses.Data.Repositories;

namespace Courses.Business.Managers
{
    public class CategoryManager : ICategoryService
    {
        private readonly IRepository<CategoryEntity> _categoryRepository;
        private readonly IRepository<CourseEntity> _courseRepository;

        public CategoryManager(IRepository<CategoryEntity> categoryRepository, IRepository<CourseEntity> courseRepository)
        {
            _categoryRepository = categoryRepository;
            _courseRepository = courseRepository;
        }

        public bool AddCategory(CategoryAddDto categoryAddDto)
        {
            var hasCategory = _categoryRepository.GetAll(x => x.Name.ToLower() == categoryAddDto.Name.ToLower()).ToList();

            if(hasCategory.Any())
            {
                return false;
            }

            var entity = new CategoryEntity()
            {
                Name = categoryAddDto.Name,
                Description = categoryAddDto.Description
            };

            _categoryRepository.Add(entity);
            return true;
        }

        public bool DeleteCategory(int id)
        {
            var firstCourse = _courseRepository.Get(x => x.CategoryId == id);

            if (firstCourse is not null)
            {
                return false;
            }

            _categoryRepository.Delete(id);
            return true;
        }

        public List<CategoryListDto> GetCategories()
        {
            var categoryEntities = _categoryRepository.GetAll().OrderBy(x => x.Name);
            var categoryDtoList = categoryEntities.Select(x => new CategoryListDto()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToList();

            return categoryDtoList;
        }

        public CategoryUpdateDto GetCategory(int id)
        {
            var entity = _categoryRepository.GetById(id);

            var categoryUpdateDto = new CategoryUpdateDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };

            return categoryUpdateDto;
        }

        public bool UpdateCategory(CategoryUpdateDto categoryUpdateDto)
        {
            var hasCategory = _categoryRepository.GetAll(x => x.Name.ToLower() == categoryUpdateDto.Name.ToLower() && x.Id != categoryUpdateDto.Id).ToList();

            if (hasCategory.Any())
            {
                return false;
            }

            var entity = _categoryRepository.GetById(categoryUpdateDto.Id);

            entity.Name = categoryUpdateDto.Name;
            entity.Description = categoryUpdateDto.Description;

            _categoryRepository.Update(entity);

            return true;
        }
    }
}
