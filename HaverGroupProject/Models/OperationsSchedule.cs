using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace HaverGroupProject.Models
{
    public class OperationsSchedule
    {
        public int ID { get; set; }

        [Display(Name = "Customer")]
        [Required(ErrorMessage = "Number of Customer is required")]
        public int SalesOrdNum { get; set; }

        [Display(Name = "External Sales Order")]
        [Required(ErrorMessage = "External Sales Order is required")]
        public int ExtSalesOrdNum { get; set; }

        [Display(Name = "Customer")]
        public int? CustomerID { get; set; }
        public Customer? Customer { get; set; }

        [Display(Name = "Machine Description")]
        [Required(ErrorMessage = "Machine Description is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only alphabetic characters are allowed")]
        public string? MachineDesc { get; set; }

        [Display(Name = "Serial #")]
        [Required(ErrorMessage = "Serial number is required")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Only numeric characters are allowed")]
        public string? SerialNum { get; set; }

        [Display(Name = "Package Release Name")]
        [Required(ErrorMessage = "Package Release Name is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only alphabetic characters are allowed")]
        public string? PackageReleaseName { get; set; }

        [Display(Name = "Kickoff Meeting")]
        [Required(ErrorMessage = "Kickoff Meeting Date is required")]
        [DataType(DataType.DateTime)]
        public DateOnly KickoffMeeting { get; set; }

        [Display(Name = "Approval Drawing")]
        [Required(ErrorMessage = "Approval Drawing is required")]
        public DateOnly ReleaseApprovalDrawing { get; set; }

        [Display(Name = "Vendor")]
        public int? VendorID { get; set; }
        public Vendor? Vendor { get; set; }

        [Display(Name = "PO#")]
        [Required(ErrorMessage = "PO Number is required")]
        public string? PONum { get; set; }

        [Display(Name = "PO Due Date")]
        [Required(ErrorMessage = "PO Due Date is required")]
        [DataType(DataType.DateTime)]
        public DateOnly PODueDate { get; set; }

        [Display(Name = "Delivery Date")]
        [Required(ErrorMessage = "Delivery Date is required")]
        [DataType(DataType.DateTime)]
        public DateOnly DeliveryDate { get; set; }
    }
}
