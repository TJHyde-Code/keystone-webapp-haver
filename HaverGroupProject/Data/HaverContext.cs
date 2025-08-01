﻿using HaverGroupProject.Models;
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
        public DbSet<OperationsScheduleMachine> OperationsScheduleMachines { get; set; }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Engineer> Engineers { get; set; }     

        public DbSet<Note> Notes { get; set; }
        public DbSet<MachineDescription> MachineDescriptions { get; set; }

        public DbSet<Employee> Employees { get; set; }          //Employees Set for user management
        public DbSet<Subscription> Subscriptions { get; set; }  //Push notifications


        //Added override so instead of deleting it simply switches Archived to true
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<Vendor>())
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.VendorArchived = true;
                }

            }
            foreach (var entry in ChangeTracker.Entries<Customer>())
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.CustomerArchived = true;
                }
            }
            return base.SaveChanges();
        }



        public DbSet<HaverGantt> HaverGantts { get; set; }
        
       


        //ModelBuilder        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //TH - 
            modelBuilder.Entity<OperationsScheduleMachine>()
               .HasKey(m => new { m.OperationsScheduleID, m.MachineDescriptionID });

            //Cascade Delete protection.

            ////Vendor Relationship
            //modelBuilder.Entity<OperationsSchedule>()
            //    .HasOne(o => o.Vendor)
            //    .WithMany(v => v.OperationsSchedules)
            //    .HasForeignKey(o => o.VendorID)
            //    .OnDelete(DeleteBehavior.Restrict);

            ////Customer Relationship
            //modelBuilder.Entity<OperationsSchedule>()
            //    .HasOne(o => o.Customer)
            //    .WithMany()
            //    .HasForeignKey(o => o.CustomerID)
            //    .OnDelete(DeleteBehavior.Restrict); //Allows null in Step1

            //MachineDescription Relationship
            //modelBuilder.Entity<OperationsSchedule>()
            //    .HasOne(o => o.MachineDescription)
            //    .WithMany()
            //    .HasForeignKey(o => o.MachineDescriptionID)
            //    .OnDelete(DeleteBehavior.Restrict); //Allows nulls in Step1

            ////Engineer RelationShip
            //modelBuilder.Entity<OperationsSchedule>()
            //    .HasOne(o => o.Engineer)
            //    .WithMany()
            //    .HasForeignKey(o => o.EngineerID)
            //    .OnDelete(DeleteBehavior.Restrict); //Allows null in Step1

            ////Notes RelationShip
            //modelBuilder.Entity<OperationsSchedule>()
            //    .HasOne(o => o.Note)
            //    .WithMany()
            //    .HasForeignKey(o => o.NoteID)
            //    .OnDelete(DeleteBehavior.Restrict);


            //Add a unique index to the Employee Email for user management
            modelBuilder.Entity<Employee>()
            .HasIndex(a => new { a.Email })
            .IsUnique();


            //Many-to-many RelationShip (OperationSchedule <-> Vendor)
            modelBuilder.Entity<OperationsScheduleVendor>()
                .HasOne(ov => ov.Vendor)
                .WithMany(v => v.OperationsScheduleVendors)
                .HasForeignKey(ov => ov.VendorID)
                .OnDelete(DeleteBehavior.Cascade); //Deletes reference if Vendor is deleted

            modelBuilder.Entity<OperationsScheduleVendor>()
                .HasOne(ov => ov.OperationsSchedule)
                .WithMany(o => o.OperationsScheduleVendors)
                .HasForeignKey(ov => ov.OperationsScheduleID)
                .OnDelete(DeleteBehavior.Cascade); //Ensures many-to-many relationship integrity  

            //Unique Index constraints.

            //Many to Many OperationsScheduleVendor Primary Key
            modelBuilder.Entity<OperationsScheduleVendor>()
                .HasKey(t => new { t.VendorID, t.OperationsScheduleID });

            //Many to Many OperationsScheduleMachine Primary Key
            modelBuilder.Entity<OperationsScheduleMachine>()
                .HasKey(t => new { t.MachineDescriptionID, t.OperationsScheduleID });
            

            //Any Vendors that has Archived = true will not show up in queries
            modelBuilder.Entity<Vendor>().HasQueryFilter(v => !v.VendorArchived);
            base.OnModelCreating(modelBuilder);

            //Any Customers that has Archived = true will not show in queries
            modelBuilder.Entity<Customer>().HasQueryFilter(c => !c.CustomerArchived);
            base.OnModelCreating(modelBuilder);


            base.OnModelCreating(modelBuilder);
        }
    }//Class close.
}//Namespace close.
