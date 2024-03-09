namespace Courses.Business.Dtos
{
    public class CourseAddDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public int CategoryId { get; set; }
        public string ImagePath { get; set; }
    }
}
