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
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public IActionResult List()
        {
            var courseDtoList = _courseService.GetCourses();
            var viewModel = courseDtoList.Select(x => new CourseListViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description?.Length > 20 ? x.Description?.Substring(0, 20) + "..." : x.Description,
                UnitPrice = x.UnitPrice,
                UnitsInStock = x.UnitsInStock,
                ImagePath = x.ImagePath?.Length > 20 ? x.ImagePath?.Substring(0, 20) + "..." : x.ImagePath,
                CategoryName = x.CategoryName,
                InstructorName = x.InstructorName,
            }).ToList();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create", new CourseFormViewModel());
        }

        [HttpPost]
        public IActionResult Create(CourseFormViewModel formData)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", formData);
            }

            var courseAddDto = new CourseAddDto()
            {
                Name = formData.Name.Trim(),
                Description = formData.Description?.Trim(),
                UnitPrice = formData.UnitPrice,
                UnitsInStock = formData.UnitsInStock,
                ImagePath = formData.ImagePath,
                CategoryId = formData.CategoryId,
                InstructorId = formData.InstructorId,
            };

            var result = _courseService.AddCourse(courseAddDto);

            if (result)
            {
                return RedirectToAction("List");
            }
            else
            {
                ViewBag.ErrorMessage = "Bu isimde bir kurs zaten mevcut.";
                return View("Create", formData);
            }
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var courseDto = _courseService.GetCourse(id);

            var viewModel = new CourseFormViewModel()
            {
                Id = courseDto.Id,
                Name = courseDto.Name,
                Description = courseDto.Description,
                UnitPrice = courseDto.UnitPrice,
                UnitsInStock = courseDto.UnitsInStock,
                ImagePath = courseDto.ImagePath,
                CategoryId = courseDto.CategoryId,
                InstructorId = courseDto.InstructorId,
            };

            return View("Update", viewModel);
        }

        [HttpPost]
        public IActionResult Update(CourseFormViewModel formData)
        {
            if (!ModelState.IsValid)
            {
                return View("Update", formData);
            }

            var courseUpdateDto = new CourseUpdateDto()
            {
                Id = formData.Id,
                Name = formData.Name,
                Description = formData.Description,
                UnitPrice = formData.UnitPrice,
                UnitsInStock = formData.UnitsInStock,
                ImagePath = formData.ImagePath,
                CategoryId = formData.CategoryId,
                InstructorId = formData.InstructorId,
            };

            var result = _courseService.UpdateCourse(courseUpdateDto);

            if (!result)
            {
                ViewBag.ErrorMessage = "Bu isimde bir kurs zaten mevcut olduğundan, güncelleme yapamazsınız.";
                return View("Update", formData);
            }

            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var result = _courseService.DeleteCourse(id);

            if (!result)
            {
                TempData["CourseErrorMessage"] = "İlgili kursta öğrenciler bulunduğundan silme işlemi gerçekleştirilemez.";

            }
            return RedirectToAction("List");
        }
    }
}