using Microsoft.EntityFrameworkCore;
using ShowTime.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.DataAccess.Configurations
{
    public class LineupConfiguration : IEntityTypeConfiguration<Lineup>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Lineup> builder)
        {
            builder.ToTable("Lineups");
            builder.HasKey(l => new { l.FestivalId, l.ArtistId });
            builder.Property(l => l.Stage)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(l => l.StartTime)
                .IsRequired();
            

        }
    }
}
