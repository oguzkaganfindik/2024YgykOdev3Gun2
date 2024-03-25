using Courses.Business.Dtos;

namespace Courses.Business.Services
{
    public interface ICourseService
    {
        bool AddCourse(CourseAddDto courseAddDto);
        List<CourseListDto> GetCourses();
        CourseUpdateDto GetCourse(int id);
        bool UpdateCourse(CourseUpdateDto courseUpdateDto);
        bool DeleteCourse(int id);
    }
}
