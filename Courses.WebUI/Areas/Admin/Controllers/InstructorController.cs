using Courses.Business.Dtos;
using Courses.Business.Services;
using Courses.WebUI.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Courses.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class InstructorController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IInstructorService _instructorService;

        public InstructorController(ICourseService courseService, IInstructorService instructorService)
        {
            _courseService = courseService;
            _instructorService = instructorService;

        }

        [HttpGet]
        public IActionResult List()
        {
            var InstructorDtoList = _instructorService.GetInstructors();

            var viewModel = InstructorDtoList.Select(x => new InstructorListViewModel()
            {
                Id = x.Id,
                Name = x.Name,

            }).ToList();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Courses = _courseService.GetCourses();
            return View("Create", new InstructorFormViewModel());
        }

        [HttpPost]
        public IActionResult Create(InstructorFormViewModel formData)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Courses = _courseService.GetCourses();
                return View("Create", formData);
            }

            var instructorAddDto = new InstructorAddDto()
            {
                Name = formData.Name,
            };

            _instructorService.AddInstructor(instructorAddDto);
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var updateInstructorDto = _instructorService.GetInstructor(id);

            var viewModel = new InstructorFormViewModel()
            {
                Id = updateInstructorDto.Id,
                Name = updateInstructorDto.Name,
            };
            ViewBag.Courses = _courseService.GetCourses();
            return View("Update", viewModel);
        }


        [HttpPost]
        public IActionResult Update(InstructorFormViewModel formData)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Courses = _courseService.GetCourses();
                return View("Update", formData);
            }


            var instructorUpdateDto = new InstructorUpdateDto()
            {
                Id = formData.Id,
                Name = formData.Name,
            };

            _instructorService.UpdateInstructor(instructorUpdateDto);
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _instructorService.DeleteInstructor(id);

            return RedirectToAction("List");
        }
    }
}
