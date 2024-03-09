using Courses.Business.Dtos;

namespace Courses.Business.Services
{
    public interface IInstructorService
    {
        bool AddInstructor(InstructorAddDto instructorAddDto);
        List<InstructorListDto> GetInstructors();
        InstructorUpdateDto GetInstructor(int id);
        bool UpdateInstructor(InstructorUpdateDto instructorUpdateDto);
        bool DeleteInstructor(int id);
    }
}
