using Courses.Business.Dtos;
using Courses.Business.Services;
using Courses.WebUI.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Courses.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ICategoryService _categoryService;

        public CourseController(ICourseService courseService, ICategoryService categoryService)
        {
            _courseService = courseService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult List()
        {
            var courseDtoList = _courseService.GetCourses();

            var viewModel = courseDtoList.Select(x => new CourseListViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                UnitPrice = x.UnitPrice,
                UnitsInStock = x.UnitsInStock,
                CategoryName = x.CategoryName,
                ImagePath = x.ImagePath

            }).ToList();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = _categoryService.GetCategories();
            return View("Create", new CourseFormViewModel());
        }

        [HttpPost]
        public IActionResult Create(CourseFormViewModel formData)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _categoryService.GetCategories();
                return View("Create", formData);
            }

            var courseAddDto = new CourseAddDto()
            {
                Name = formData.Name,
                Description = formData.Description,
                UnitPrice = formData.UnitPrice,
                UnitsInStock = formData.UnitsInStock,
                CategoryId = formData.CategoryId,
            };

            _courseService.AddCourse(courseAddDto);
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var updateCourseDto = _courseService.GetCourseById(id);

            var viewModel = new CourseFormViewModel()
            {
                Id = updateCourseDto.Id,
                Name = updateCourseDto.Name,
                Description = updateCourseDto.Description,
                UnitPrice = updateCourseDto.UnitPrice,
                UnitsInStock = updateCourseDto.UnitsInStock,
                CategoryId = updateCourseDto.CategoryId,
            };

            ViewBag.ImagePath = updateCourseDto.ImagePath;

            ViewBag.Categories = _categoryService.GetCategories();
            return View("Update", viewModel);
        }


        [HttpPost]
        public IActionResult Update(CourseFormViewModel formData)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _categoryService.GetCategories();
                return View("Update", formData);
            }


            var courseUpdateDto = new CourseUpdateDto()
            {
                Id = formData.Id,
                Name = formData.Name,
                Description = formData.Description,
                UnitPrice = formData.UnitPrice,
                UnitsInStock = formData.UnitsInStock,
                CategoryId = formData.CategoryId,
            };

            _courseService.UpdateCourse(courseUpdateDto);
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _courseService.DeleteCourse(id);

            return RedirectToAction("List");
        }
    }
}
