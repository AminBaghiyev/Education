using Education.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Education.DL.Configurations;

public class NewsConfiguration : IEntityTypeConfiguration<News>
{
    public void Configure(EntityTypeBuilder<News> builder)
    {
        builder
            .Property(e => e.Title).HasMaxLength(50);

        builder
            .Property(e => e.Description).HasMaxLength(255);
    }
}
