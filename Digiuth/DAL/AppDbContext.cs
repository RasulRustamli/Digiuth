using Digiuth.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Digiuth.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Bio> Bios { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<WatchUs> WatchUs { get; set; }
        public DbSet<MainCategory> MainCategories { get; set; }
        public DbSet<ChildCategory> ChildCategories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseSubject> CourseSubjects { get; set; }
        public DbSet<EducationLanguage> EducationLanguages { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<OurEvent> OurEvents { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<CourseContent> CourseContents { get; set; }
        public DbSet<CourseVideo> CourseVideos { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Bio
            builder.Entity<Bio>().HasData(new Bio
            {
                Id = 1,
                Phone = "1800-121-3637",
                Phone2= "+91-7052-101-786",                
                Address = "1247/Plot No. 39, 15th Phase, LHB Colony, Kanpur",
                Email = "info@example.com",
                Email2= "help@example.com",
                Instagram = "instagram.com",
                Facebook = "facebook.com",
                Twitter = "twitter.com",
                AboutUs= "Cras faucibus ornare ipsum, non luctus leo imperdiet sit amet. Praesent egestas orci eu risus iaculis luctus. Phasellus maximus orci metus. Nullam enim ex, facilisis at lacinia sed, luctus vitae dolor."
            });

            // Testimonial
            builder.Entity<Testimonial>().HasData(
            new Testimonial
            {
                Id = 1,
                FullName = "Eity Akhter",
                Image = "testi_avatar.png",
                Position = "Student",
                Description = "Etiam quis lacinia ipsum. Aliquam blandit, mauris nec molestie interdum, quam massa finibus turpis, ut eleifend tellus massa eget nunc. Maecenas luctus diam id augue fringilla ornare. Sed varius massa non sem rutrum malesuada."
            },
            new Testimonial
            {
                Id = 2,
                FullName = "Margie R. Robinson",
                Image = "testi_avatar.png",
                Position = "Web Developer",
                Description = "Etiam quis lacinia ipsum. Aliquam blandit, mauris nec molestie interdum, quam massa finibus turpis, ut eleifend tellus massa eget nunc. Maecenas luctus diam id augue fringilla ornare. Sed varius massa non sem rutrum malesuada."
            },
            new Testimonial
            {
                Id = 3,
                FullName = "Margie R. Robinson",
                Image = "testi_avatar.png",
                Position = "Web Developer",
                Description = "Etiam quis lacinia ipsum. Aliquam blandit, mauris nec molestie interdum, quam massa finibus turpis, ut eleifend tellus massa eget nunc. Maecenas luctus diam id augue fringilla ornare. Sed varius massa non sem rutrum malesuada."
            });

            // About Us
            builder.Entity<AboutUs>().HasData(new AboutUs
            {
                Id = 1,
                Image = "about_img.png",
                ExperienceYear=50,
                Title = "Welcome To Online Class Educato",
                Description = "Curabitur tristique, sem id sagittis varius, lacus ligula mollis dui, ac condimentum felis metus ut nulla. Aenean ut ultricies turpis, sed sollicitudin eros. Aliquam quis dui ut diam lobortis dignissim ut aliquet ex.",
                AboutContent= "Nulla pellentesque posuere metus, sed hendrerit purus venenatis in. Sed at vestibulum magna.",
                AboutContent2= "Etiam quis lacinia ipsum. Aliquam blandit, mauris nec molestie interdum, quam massa finibus turpis, ut eleifend tellus massa eget nunc."
            });
            
            // Watch Us
            builder.Entity<WatchUs>().HasData(new WatchUs
            {
                Id = 1,
                Title = "Start Growing With Community",
                VideoUrl= "https://www.youtube.com/watch?v=VzyYit-px3o"
            });

            // Main Category
            builder.Entity<MainCategory>().HasData(
            new MainCategory
            {
                Id = 1,
                Name = "Digital Marketing",
                LongDesc= "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                ShortDesc= "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
                PhotoUrl= "29e75464-d4fc-4185-ab67-fe0c9f006017programming.jpg",
                VideoUrl= "https://www.youtube.com/watch?v=zOjov-2OZ0E",
                WhyChooseCourse= "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."
            },
            new MainCategory
            {
                Id = 2,
                Name = "Programming",
                LongDesc = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                ShortDesc = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
                PhotoUrl = "29e75464-d4fc-4185-ab67-fe0c9f006017programming.jpg",
                VideoUrl = "https://www.youtube.com/watch?v=zOjov-2OZ0E",
                WhyChooseCourse = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."
            },
            new MainCategory
           {
               Id = 3,
               Name = "Design",
                LongDesc = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                ShortDesc = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
                PhotoUrl = "29e75464-d4fc-4185-ab67-fe0c9f006017programming.jpg",
                VideoUrl = "https://www.youtube.com/watch?v=zOjov-2OZ0E",
                WhyChooseCourse = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."
            },
            new MainCategory
           {
               Id = 4,
               Name = "3d Render",
                LongDesc = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                ShortDesc = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
                PhotoUrl = "29e75464-d4fc-4185-ab67-fe0c9f006017programming.jpg",
                VideoUrl = "https://www.youtube.com/watch?v=zOjov-2OZ0E",
                WhyChooseCourse = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."
            });

            // Child Category
            builder.Entity<ChildCategory>().HasData(
            new ChildCategory
            {
                Id=1,
                MainCategoryId=2,
                Name="C# course"
            },
            new ChildCategory
            {
                Id = 2,
                MainCategoryId = 1,
                Name = "Magazine"
            },
            new ChildCategory
            {
                Id = 3,
                MainCategoryId = 4,
                Name = "House models"
            },
            new ChildCategory
            {
                Id = 4,
                MainCategoryId = 3,
                Name = "Website Design"
            });

            // Education Languages
            builder.Entity<EducationLanguage>().HasData(
            new EducationLanguage
            {
                Id = 1,
                Name = "English"
            },
            new EducationLanguage
            {
                Id = 2,
                Name = "Russian"
            },
            new EducationLanguage
            {
                Id = 3,
                Name = "Azerbaijan"
            });

        }
    }
}