using Courses.Business.Services;
using Courses.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Courses.WebUI.ViewComponents
{
    public class CoursesViewComponent : ViewComponent
    {
        private readonly ICourseService _courseService;

        public CoursesViewComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public IViewComponentResult Invoke()
        {

            var courseDtoList = _courseService.GetCourses();
            var viewModel = courseDtoList.Select(x => new CourseListViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                UnitPrice = x.UnitPrice,
                UnitsInStock = x.UnitsInStock,
                ImagePath = x.ImagePath,
                CategoryName = x.CategoryName,
                InstructorName = x.InstructorName,
            }).ToList();

            return View(viewModel);
        }
    }
}
