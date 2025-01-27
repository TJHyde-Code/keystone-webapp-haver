using HaverGroupProject.Models;
using Microsoft.EntityFrameworkCore;

namespace HaverGroupProject.Data
{
    public class HaverContext : DbContext
    {
        //Constructor
        public HaverContext(DbContextOptions<HaverContext> options) : base(options)
        {

        }

        //DBSets
        public DbSet<OperationsSchedule> OperationsSchedules { get; set; }

        public DbSet<Vendor> Vendors { get; set; }

        public DbSet<OperationsScheduleVendor> OperationsScheduleVendors { get; set; }

        public DbSet<Customer> Customers { get; set; }

        //ModelBuilder        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Cascade Delete protection.
            modelBuilder.Entity<OperationsSchedule>()
                .HasOne(o => o.Vendor)
                .WithMany(v => v.OperationsSchedules)
                .HasForeignKey(o => o.VendorID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OperationsScheduleVendor>()
                .HasOne(ov => ov.Vendor)
                .WithMany(v => v.OperationsScheduleVendors)
                .HasForeignKey(ov => ov.VendorID)
                .OnDelete(DeleteBehavior.Cascade);

            //Unique Index constraints.

            //Many to Many OperationsScheduleVendor Primary Key
            modelBuilder.Entity<OperationsScheduleVendor>()
                .HasKey(t => new { t.VendorID, t.OperationsScheduleID });

            base.OnModelCreating(modelBuilder);
        }
    }//Class close.
}//Namespace close.
