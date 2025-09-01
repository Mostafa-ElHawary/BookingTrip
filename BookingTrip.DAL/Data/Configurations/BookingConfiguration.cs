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
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.PickupLocation)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.DropoffLocation)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.Fare)
                .HasColumnType("decimal(18, 2)");

            builder.Property(b => b.Status)
                .IsRequired();

            builder.HasOne(b => b.Trip)
                .WithMany(t => t.Bookings)
                .HasForeignKey(b => b.TripId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting trip if it has associated bookings

            builder.HasOne(b => b.Rider)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RiderId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting rider if they have associated bookings

            builder.HasOne(b => b.Payment)
                .WithOne(p => p.Booking)
                .HasForeignKey<Payment>(p => p.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.Rating)
                .WithOne(r => r.Booking)
                .HasForeignKey<Rating>(r => r.BookingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
