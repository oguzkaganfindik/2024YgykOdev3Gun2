﻿using Courses.Business.Dtos;

namespace Courses.Business.Services
{
    public interface ICategoryService
    {
        bool AddCategory(CategoryAddDto categoryAddDto);
        List<CategoryListDto> GetCategories();
        CategoryUpdateDto GetCategory(int id);
        bool UpdateCategory(CategoryUpdateDto categoryUpdateDto);
        bool DeleteCategory(int id);
    }
}
