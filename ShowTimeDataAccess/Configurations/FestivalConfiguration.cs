using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShowTime.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.DataAccess.Configurations
{
    public class FestivalConfiguration : IEntityTypeConfiguration<Festival>
    {
        public void Configure(EntityTypeBuilder<Festival> builder)
        {
            builder.ToTable("Festivals");
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id)
                .ValueGeneratedOnAdd();
            builder.Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(f => f.Location)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(f => f.StartDate)
                .IsRequired();
            builder.Property(f => f.EndDate)
                .IsRequired();
            builder.Property(f => f.SplashArt)
                .IsRequired();
            builder.Property(f => f.capacity)
                .IsRequired();

        }
    }
}
