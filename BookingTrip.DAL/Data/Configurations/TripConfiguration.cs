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
    public class TripConfiguration : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.StartLocation)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.EndLocation)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.StartTime)
                .IsRequired();

            builder.Property(t => t.AvailableSeats)
                .IsRequired();

            builder.Property(t => t.SuggestedFare)
                .HasColumnType("decimal(18, 2)");

            builder.Property(t => t.Status)
                .IsRequired();

            builder.HasOne(t => t.Driver)
                .WithMany(d => d.Trips)
                .HasForeignKey(t => t.DriverId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting driver if they have associated trips

            builder.HasMany(t => t.Bookings)
                .WithOne(b => b.Trip)
                .HasForeignKey(b => b.TripId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting trip if it has associated bookings
        }
    }
}
