using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTrip.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookingTrip.DAL.Data.Configurations
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Score)
                .IsRequired();

            builder.Property(r => r.Feedback)
                .HasMaxLength(500);

            builder.Property(r => r.RatingDate)
                .IsRequired();

            builder.HasOne(r => r.Booking)
                .WithOne(b => b.Rating)
                .HasForeignKey<Rating>(r => r.BookingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
