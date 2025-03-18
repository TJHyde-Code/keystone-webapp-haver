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


        //Remove
        [Display(Name = "External Sales Order")]
        public int ExtSalesOrdNum { get; set; }


        //Remove as it's not needed (Including in seed data)
        [Display(Name = "Package Release Name")]
        public string? PackageReleaseName { get; set; }

        //Additional data capture dates added here.

        [Display(Name = "Kickoff Meeting")]
        [DataType(DataType.Date)]
        public DateTime? KickoffMeeting { get; set; }

        [Display(Name = "Approval Drawing")]
        [DataType(DataType.Date)]
        public DateTime? ReleaseApprovalDrawing { get; set; }

        

        [Display(Name = "PO#")]
        public string? PONum { get; set; }
        [Display(Name = "Production Order #")]
        public string? ProductionOrderNumber { get; set; }

        [Display(Name = "PO Due Date")]
        [DataType(DataType.Date)]
        public DateTime? PODueDate { get; set; }

        [Display(Name = "Delivery Date")]
        [DataType(DataType.Date)]
        public DateTime? DeliveryDate { get; set; }


        /// <summary>
        /// Navigations
        /// </summary>
        #region Navigaions
        [Display(Name = "Engineer")]
        [ForeignKey("Engineer")]
        public int? EngineerID { get; set; }
        public Engineer? Engineer { get; set; }

        [Display(Name = "Machine Desc.")]
        [ForeignKey("MachineDescription")]
        public int? MachineDescriptionID { get; set; }
        public MachineDescription? MachineDescription { get; set; }

        [Display(Name = "Customer")]
        [ForeignKey("Customer")]
        public int? CustomerID { get; set; }
        public Customer? Customer { get; set; }

        [Display(Name = "Vendor")]
        [ForeignKey("Vendor")]
        public int? VendorID { get; set; }
        public Vendor? Vendor { get; set; }

        [Display(Name = "Notes")]
        [ForeignKey("Note")]
        public int? NoteID { get; set; }
        public Note? Note { get; set; }

        [Display(Name = "Vendors")]
        public ICollection<OperationsScheduleVendor> OperationsScheduleVendors { get; set; } = new HashSet<OperationsScheduleVendor>();

        [Display(Name = "Machines")]
        public ICollection<OperationsScheduleMachine> OperationsScheduleMachines { get; set; } = new HashSet<OperationsScheduleMachine>();

        
        #endregion

    }
}
