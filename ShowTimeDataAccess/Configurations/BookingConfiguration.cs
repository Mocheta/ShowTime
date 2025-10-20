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
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("Bookings");
            builder.HasKey(b => new { b.FestivalId, b.UserId });
            builder.Property(b => b.type)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasOne(b => b.Ticket)
                .WithOne(t => t.Booking)
                .OnDelete(DeleteBehavior.NoAction); // Assuming you want to delete Booking when Ticket is deleted

            // Assuming Festival and User are configured elsewhere
            //builder.HasOne<Festival>()
            //    .WithMany()
            //    .HasForeignKey(b => b.FestivalId);
            //builder.HasOne<User>()
            //    .WithMany()
            //    .HasForeignKey(b => b.UserId);
        }
    }
}
