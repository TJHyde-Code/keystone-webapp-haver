using System.ComponentModel.DataAnnotations;

namespace HaverGroupProject.Models
{
    public class Customer
    {
        public int ID { get; set; }

        [Display(Name = "Customer's Name")]
        [Required(ErrorMessage = "Customer Name is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only alphabetic characters are allowed")]
        public string? CustomerName { get; set; }

        [Display(Name = "Release Date")]
        [Required(ErrorMessage = "Release Date is required")]
        [DataType(DataType.DateTime)]
        public DateOnly ReleaseDate { get; set; }

        [Display(Name = "Customer Address")]
        [Required(ErrorMessage = "Customer Address is required")]
        public string? CustomerAddress { get; set; }

        [Display(Name = "Customer Contact Name")]
        [Required(ErrorMessage = "Customer Contact Name is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only alphabetic characters are allowed")]
        public string? CustomerContactName { get; set; }

        [Display(Name = "Customer Contact Name")]
        [Required(ErrorMessage = "Customer Contact Name is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only alphabetic characters are allowed")]
        public string? CustomerEmail { get; set; }

        //MT-Added Customer Phone Number
        [Display(Name = "Customer Phone Number")]
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(10)]
        public string CustomerPhone { get; set; } = "";

        public ICollection<OperationsSchedule>? OperationsSchedule { get; set; } = new HashSet<OperationsSchedule>();

    }
}
