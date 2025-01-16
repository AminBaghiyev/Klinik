using Klinik.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Klinik.DL.Configurations;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder
            .Property(x => x.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(x => x.LastName)
            .HasMaxLength(50) 
            .IsRequired();

        builder
            .Property(x => x.FacebookURL)
            .HasMaxLength(500)
            .IsRequired(false);

        builder
            .Property(x => x.InstagramURL)
            .HasMaxLength(500)
            .IsRequired(false);

        builder
            .Property(x => x.TwitterURL)
            .HasMaxLength(500)
            .IsRequired(false);
    }
}
