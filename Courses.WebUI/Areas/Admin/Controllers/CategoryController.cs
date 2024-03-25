﻿using Courses.Business.Dtos;
using Courses.Business.Services;
using Courses.WebUI.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Courses.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult List()
        {
            var categoryDtoList = _categoryService.GetCategories();
            var viewModel = categoryDtoList.Select(x => new CategoryListViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description?.Length > 20 ? x.Description?.Substring(0, 20) + "..." : x.Description
            }).ToList();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create", new CategoryFormViewModel());
        }

        [HttpPost]
        public IActionResult Create(CategoryFormViewModel formData)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", formData);
            }

            var categoryAddDto = new CategoryAddDto()
            {
                Name = formData.Name.Trim(),
                Description = formData.Description?.Trim()
            };

            var result = _categoryService.AddCategory(categoryAddDto);

            if (result)
            {
                return RedirectToAction("List");
            }
            else
            {
                ViewBag.ErrorMessage = "Bu isimde bir kategori zaten mevcut.";
                return View("Create", formData);
            }
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var categoryDto = _categoryService.GetCategory(id);

            var viewModel = new CategoryFormViewModel()
            {
                Id = categoryDto.Id,
                Name = categoryDto.Name,
                Description = categoryDto.Description
            };

            return View("Update", viewModel);
        }

        [HttpPost]
        public IActionResult Update(CategoryFormViewModel formData)
        {
            if (!ModelState.IsValid)
            {
                return View("Update", formData);
            }

            var categoryUpdateDto = new CategoryUpdateDto()
            {
                Id = formData.Id,
                Name = formData.Name,
                Description = formData.Description
            };

            var result = _categoryService.UpdateCategory(categoryUpdateDto);

            if (!result)
            {
                ViewBag.ErrorMessage = "Bu isimde bir kategori zaten mevcut olduğundan, güncelleme yapamazsınız.";
                return View("Update", formData);
            }

            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var result = _categoryService.DeleteCategory(id);

            if (!result)
            {
                TempData["CategoryErrorMessage"] = "İlgili kategoride kurslar bulunduğundan silme işlemi gerçekleştirilemez.";

            }
            return RedirectToAction("List");
        }
    }
}