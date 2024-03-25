using Courses.Data.Entities;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

namespace Courses.Data.Context
{
    public class CoursesContext : DbContext
    {
        private readonly IDataProtector _dataProtector;
        public CoursesContext(DbContextOptions<CoursesContext> options, IDataProtectionProvider dataProtectionProvider) : base(options)
        {
            _dataProtector = dataProtectionProvider.CreateProtector("security");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new InstructorConfiguration());

            modelBuilder.Entity<CategoryEntity>().HasData(new List<CategoryEntity>()
            {
                new CategoryEntity()
                {
                    Id = 1,
                    Name = "Programlama",
                    Description = "qwerty",
                }
            });

            modelBuilder.Entity<InstructorEntity>().HasData(new List<InstructorEntity>()
            {
                new InstructorEntity()
                {
                    Id = 2,
                    Name = "Engin Demirog",
                }
            });

            modelBuilder.Entity<CourseEntity>().HasData(new List<CourseEntity>()
            {
                new CourseEntity()
                {
                    Id = 3,
                    Name = "Yazılım Geliştirici Yetiştirme Kampı (C# + ANGULAR)",
                    Description = "2 ay sürecek Yazılım Geliştirici Yetiştirme Kampımızın takip, döküman ve duyurularını buradan yapacağız.",
                    UnitPrice = 1,
                    UnitsInStock = 999,
                    ImagePath = "https://process.fs.teachablecdn.com/ADNupMnWyR7kCWRvm76Laz/resize=width:705/https://www.filepicker.io/api/file/Zk7d1MdoSJ6cEShVbfd0",
                    CategoryId = 1,
                    InstructorId = 2
                }
            });

            modelBuilder.Entity<CourseEntity>().HasData(new List<CourseEntity>()
            {
                new CourseEntity()
                {
                    Id = 4,
                    Name = "Senior Yazılım Geliştirici Yetiştirme Kampı (.NET)",
                    Description = "Senior Yazılım Geliştirici Yetiştirme Kampımızın takip, döküman ve duyurularını buradan yapacağız.",
                    UnitPrice = 1,
                    UnitsInStock = 999,
                    ImagePath = "https://process.fs.teachablecdn.com/ADNupMnWyR7kCWRvm76Laz/resize=width:705/https://cdn.filestackcontent.com/ujZYJ3lwSOihtnbP9LsG",
                    CategoryId = 1,
                    InstructorId = 2
                }
            });

            modelBuilder.Entity<CourseEntity>().HasData(new List<CourseEntity>()
            {
                new CourseEntity()
                {
                    Id = 5,
                    Name = "2024 Yazılım Geliştirici Yetiştirme Kampı (C#)",
                    Description = "2 ay sürecek Yazılım Geliştirici Yetiştirme Kampımızın takip, döküman ve duyurularını buradan yapacağız.",
                    UnitPrice = 1,
                    UnitsInStock = 999,
                    ImagePath = "https://process.fs.teachablecdn.com/ADNupMnWyR7kCWRvm76Laz/resize=width:705/https://cdn.filestackcontent.com/We86Zc3xQy6FUqhyBJJc",
                    CategoryId = 1,
                    InstructorId = 2
                }
            });

            modelBuilder.Entity<CourseEntity>().HasData(new List<CourseEntity>()
            {
                new CourseEntity()
                {
                    Id = 6,
                    Name = "Programlamaya Giriş için Temel Kurs",
                    Description = "PYTHON, JAVA, C# gibi tüm programlama dilleri için temel programlama mantığını anlaşılır örneklerle öğrenin.",
                    UnitPrice = 1,
                    UnitsInStock = 999,
                    ImagePath = "https://process.fs.teachablecdn.com/ADNupMnWyR7kCWRvm76Laz/resize=width:705/https://www.filepicker.io/api/file/BBLmG3XFTtm8XBTrzg4v",
                    CategoryId = 1,
                    InstructorId = 2
                }
            });

            modelBuilder.Entity<UserEntity>().HasData(new List<UserEntity>()
            {
                new UserEntity()
                {
                    Id = 7,
                    FirstName = "Şebnem",
                    LastName = "Ferah",
                    Email = "admin@test.com",
                    Password = _dataProtector.Protect("123"),
                    UserType = Enums.UserTypeEnum.Admin                  
                }
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<CategoryEntity> Categories => Set<CategoryEntity>();
        public DbSet<CourseEntity> Courses => Set<CourseEntity>();
        public DbSet<InstructorEntity> Instructors => Set<InstructorEntity>();
    }
}
