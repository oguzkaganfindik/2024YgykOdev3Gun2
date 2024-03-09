using Courses.Business.Dtos;

namespace Courses.Business.Services
{
    public interface ICourseService
    {
        void AddCourse(CourseAddDto courseAddDto);

        List<CourseListDto> GetCourses();

        CourseUpdateDto GetCourseById(int id);

        void UpdateCourse(CourseUpdateDto courseUpdateDto);

        void DeleteCourse(int id);

        List<CourseListDto> GetCoursesByCategoryId(int? categoryId = null);
    }
}
