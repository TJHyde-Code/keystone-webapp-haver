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
        public DbSet<Engineer> Engineers { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Customer> Customers { get; set; }        
        public DbSet<Note> Notes { get; set; }
        public DbSet<MachineDescription> MachineDescriptions { get; set; }

        //ModelBuilder        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Cascade Delete protection.
            modelBuilder.Entity<OperationsSchedule>()
                .HasOne(o => o.Vendor)
                .WithMany(v => v.OperationsSchedules)
                .HasForeignKey(o => o.VendorID)
                .OnDelete(DeleteBehavior.Restrict);

            //Unique Index constraints.


            base.OnModelCreating(modelBuilder);
        }
    }//Class close.
}//Namespace close.
