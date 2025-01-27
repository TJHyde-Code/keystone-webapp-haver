using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace HaverGroupProject.Models
{
    public class OperationsSchedule
    {
        public int ID { get; set; }

        [Display(Name = "Sales Order")]
        public int SalesOrdNum { get; set; }

        [Display(Name = "External Sales Order")]
        public int ExtSalesOrdNum { get; set; }

        [Display(Name = "Customer")]
        public int? CustomerID { get; set; }
        public Customer? Customer { get; set; }

        [Display(Name = "Machine Description")]
        public string MachineDesc { get; set; }

        [Display(Name = "Serial #")]
        public string SerialNum { get; set; }

        [Display(Name = "Package Release Name")]
        public string PackageReleaseName { get; set; }

        [Display(Name = "Kickoff Meeting")]
        public DateOnly KickoffMeeting { get; set; }

        [Display(Name = "Approval Drawing")]
        public DateOnly ReleaseApprovalDrawing { get; set; }

        [Display(Name = "Vendor")]
        public int? VendorID { get; set; }
        public Vendor? Vendor { get; set; }

        [Display(Name = "PO#")]
        public string PONum { get; set; }

        [Display(Name = "PO Due Date")]
        public DateOnly PODueDate { get; set; }

        [Display(Name = "Delivery Date")]
        public DateOnly DeliveryDate { get; set; }

        [Display(Name = "Vendors")]
        public ICollection<OperationsScheduleVendor> OperationsScheduleVendors { get; set; } = new HashSet<OperationsScheduleVendor>();
    }
}
