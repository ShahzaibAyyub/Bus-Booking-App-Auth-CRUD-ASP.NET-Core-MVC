using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using finalDeliverable.Models;
using Microsoft.EntityFrameworkCore;

namespace finalDeliverable.Data
{
    public class BookingContext : DbContext
    {

        public BookingContext(DbContextOptions<BookingContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .HasOne(u => u.user)
                .WithMany(b => b.BookingHistory)
                .HasForeignKey(p => p.userIdForeignkey);


            modelBuilder.Entity<Booking>()
                .HasOne(u => u.bus)
                .WithMany(b => b.BusBookingHistory)
                .HasForeignKey(p => p.busIdForeignkey);
        }

        public DbSet<finalDeliverable.Models.Bus> Bus { get; set; }

        public DbSet<finalDeliverable.Models.Customer> User { get; set; }

        public DbSet<finalDeliverable.Models.Booking> Booking { get; set; }
        public DbSet<finalDeliverable.Models.Admin> Admin { get; set; }



    }
}
