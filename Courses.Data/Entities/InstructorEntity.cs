using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Courses.Data.Entities
{
    public class InstructorEntity :  BaseEntity
    {
        public string Name { get; set; }
        public List<CourseEntity> Courses { get; set; }
    }

    public class InstructorConfiguration : BaseConfiguration<InstructorEntity>
    {
        public override void Configure(EntityTypeBuilder<InstructorEntity> builder)
        {

            builder.Property(x => x.Name)
                .HasMaxLength(40);

            base.Configure(builder);
        }
    }
}
