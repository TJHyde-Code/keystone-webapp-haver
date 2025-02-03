using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [ForeignKey("Customer")]
        public int? CustomerID { get; set; }
        public Customer? Customer { get; set; }


        //Comment this out after seeding data updated to use the Machine Desc table
        [Display(Name = "Machine Description")]
        public string? MachineDesc { get; set; }


        [Display(Name = "Serial #")]
        public string? SerialNum { get; set; }

        [Display(Name = "Package Release Name")]
        public string? PackageReleaseName { get; set; }

        [Display(Name = "Kickoff Meeting")]
        [DataType(DataType.Date)]
        public DateOnly? KickoffMeeting { get; set; }

        [Display(Name = "Approval Drawing")]
        [DataType(DataType.Date)]
        public DateOnly? ReleaseApprovalDrawing { get; set; }

        [Display(Name = "Vendor")]
        [ForeignKey("Vendor")]
        public int? VendorID { get; set; }
        public Vendor? Vendor { get; set; }

        [Display(Name = "PO#")]
        public string? PONum { get; set; }
        [Display(Name = "Production Order #")]
        public string? ProductionOrderNumber { get; set; }

        [Display(Name = "PO Due Date")]
        [DataType(DataType.Date)]
        public DateOnly? PODueDate { get; set; }

        [Display(Name = "Delivery Date")]
        [DataType(DataType.Date)]
        public DateOnly? DeliveryDate { get; set; }

        [Display(Name = "Engineer")]
        [ForeignKey("Engineer")]
        public int? EngineerID { get; set; }
        public Engineer? Engineer { get; set; }

        [Display(Name = "Machine Desc.")]
        [ForeignKey("MachineDescription")]
        public int? MachineDescriptionID { get; set; }
        public MachineDescription? MachineDescription { get; set; }

        [Display(Name = "Notes")]
        [ForeignKey("Note")]
        public int? NoteID { get; set; }
        public Note? Note { get; set; }

        [Display(Name = "Vendors")]
        public ICollection<OperationsScheduleVendor> OperationsScheduleVendors { get; set; } = new HashSet<OperationsScheduleVendor>();

    }
}
