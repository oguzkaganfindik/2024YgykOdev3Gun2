namespace Courses.WebUI.Models
{
    public class CourseListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public string CategoryName { get; set; }
        public string InstructorName { get; set; }
        public string ImagePath { get; set; }

    }
}
