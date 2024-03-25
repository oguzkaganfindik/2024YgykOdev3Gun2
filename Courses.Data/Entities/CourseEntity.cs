using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Courses.Data.Entities
{
    public class CourseEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public string ImagePath { get; set; }
        public int CategoryId { get; set; }
        public int InstructorId { get; set; }


        // Relational Property

        public List<UserEntity> Users { get; set; }
        public CategoryEntity Category { get; set; }
        public InstructorEntity Instructor { get; set; }

    }

    public class CourseConfiguration : BaseConfiguration<CourseEntity>
    {
        public override void Configure(EntityTypeBuilder<CourseEntity> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(500);

            builder.Property(x => x.Description)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(x => x.UnitPrice)
                .IsRequired(false);

            builder.Property(x => x.ImagePath)
                .IsRequired(false);

            base.Configure(builder);
        }
    }
}
