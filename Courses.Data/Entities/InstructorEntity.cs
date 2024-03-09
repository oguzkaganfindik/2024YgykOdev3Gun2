using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Courses.Data.Entities
{
    public class InstructorEntity :  BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<CourseEntity> Courses { get; set; }
    }

    public class InstructorConfiguration : BaseConfiguration<InstructorEntity>
    {
        public override void Configure(EntityTypeBuilder<InstructorEntity> builder)
        {

            //builder.Property(x => x.FirstName)
            //    .HasMaxLength(40);

            //builder.Property(x => x.LastName)
            //    .IsRequired(false);

            base.Configure(builder);
        }
    }
}
