using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SortingFilteringPaging.Models;

namespace SortingFilteringPaging.Data.Configuration
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasData(
                new Enrollment { EnrollmentID = 1, StudentID = 1, CourseID = 1050, Grade = Grade.A },
                new Enrollment { EnrollmentID = 2, StudentID = 1, CourseID = 4022, Grade = Grade.C },
                new Enrollment { EnrollmentID = 3, StudentID = 1, CourseID = 4041, Grade = Grade.B },
                new Enrollment { EnrollmentID = 4, StudentID = 2, CourseID = 1045, Grade = Grade.B },
                new Enrollment { EnrollmentID = 5, StudentID = 2, CourseID = 3141, Grade = Grade.F },
                new Enrollment { EnrollmentID = 6, StudentID = 2, CourseID = 2021, Grade = Grade.F },
                new Enrollment { EnrollmentID = 7, StudentID = 3, CourseID = 1050 },
                new Enrollment { EnrollmentID = 8, StudentID = 4, CourseID = 1050 },
                new Enrollment { EnrollmentID = 9, StudentID = 4, CourseID = 4022, Grade = Grade.F },
                new Enrollment { EnrollmentID = 10, StudentID = 5, CourseID = 4041, Grade = Grade.C },
                new Enrollment { EnrollmentID = 11, StudentID = 6, CourseID = 1045 },
                new Enrollment { EnrollmentID = 12, StudentID = 7, CourseID = 3141, Grade = Grade.A }
            );
        }
    }
}
