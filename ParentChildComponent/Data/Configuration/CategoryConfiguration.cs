using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ParentChildComponent.Models;

namespace ParentChildComponent.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category { Id = 1, Name = "category1", ParentId = null },
                    new Category { Id = 2, Name = "category2", ParentId = 1 },
                    new Category { Id = 3, Name = "category3", ParentId = 1 },
                new Category { Id = 4, Name = "category4", ParentId = null },
                    new Category { Id = 5, Name = "category5", ParentId = 4 },
                new Category { Id = 6, Name = "category6", ParentId = null },
                    new Category { Id = 7, Name = "category7", ParentId = 6 },
                        new Category { Id = 8, Name = "category8", ParentId = 7 },
                    new Category { Id = 9, Name = "category9", ParentId = 6 },
                        new Category { Id = 10, Name = "category10", ParentId = 9 },
                            new Category { Id = 11, Name = "category11", ParentId = 10 },
                                new Category { Id = 12, Name = "category12", ParentId = 11 }
            );
        }
    }
}
