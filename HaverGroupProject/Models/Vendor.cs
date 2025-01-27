using System.ComponentModel.DataAnnotations;

namespace HaverGroupProject.Models
{
    public class Vendor
    {
        public int ID { get; set; }

        [Display(Name = "Vendor Name ")]
        [Required(ErrorMessage = "Vendor Name is Required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only alphabetic characters are allowed")]
        public string? VendorName { get; set; }

        [Display(Name = "Vendor Address")]
        [Required(ErrorMessage = "Vendor Address is Required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only alphabetic characters are allowed")]
        public string? VendorAddress { get; set; }

        [Display(Name = "Vendor Contact Name")]
        [Required(ErrorMessage = "Vendor Contact Name is Required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only alphabetic characters are allowed")]
        public string? VendorContactName { get; set; }

        [Display(Name = "Vendor Phone")]
        [Required(ErrorMessage = "Vendor Phone is Required")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Only numeric characters are allowed")]
        public string? VendorPhone { get; set; }

        [Display(Name = "Vendor Email")]
        [Required(ErrorMessage = "Vendor Email is Required")]
        public string? VendorEmail { get; set; }

        public ICollection<OperationsSchedule>? OperationsSchedules { get; set; } = new HashSet<OperationsSchedule>();

        public ICollection<OperationsScheduleVendor>? OperationsScheduleVendors { get; set; } = new HashSet<OperationsScheduleVendor>();
    }
}
