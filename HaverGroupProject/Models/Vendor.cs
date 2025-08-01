﻿using System.ComponentModel.DataAnnotations;

namespace HaverGroupProject.Models
{
    public class Vendor
    {
        public int ID { get; set; }

        [Display(Name = "Vendor Name ")]
        [Required(ErrorMessage = "Vendor name is required")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only alphabetic characters are allowed")]
        public string? VendorName { get; set; }

        [Display(Name = "Vendor Address")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only alphabetic characters are allowed")]
        public string? VendorAddress { get; set; }

        [Display(Name = "Vendor Contact Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only alphabetic characters are allowed")]
        public string? VendorContactName { get; set; }

        [Display(Name = "Vendor Phone")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Only numeric characters are allowed")]
        public string? VendorPhone { get; set; }

        [Display(Name = "Vendor Email")]
        public string? VendorEmail { get; set; }

        //Inserting Archive for Vendors property
        public bool VendorArchived { get; set; }

        public ICollection<OperationsSchedule>? OperationsSchedules { get; set; } = new HashSet<OperationsSchedule>();

        public ICollection<OperationsScheduleVendor>? OperationsScheduleVendors { get; set; } = new HashSet<OperationsScheduleVendor>();

        public ICollection<HaverGantt> HaverGantts { get; set; } = new HashSet<HaverGantt>();
    }
}
