using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Security.Permissions;

namespace HaverGroupProject.Models
{
    public class OperationsSchedule
    {
        public int ID { get; set; }

        [Display(Name = "Sales Order")]
        public int SalesOrdNum { get; set; }

		[Display(Name = "PO#")]
		public string? PONum { get; set; }
		[Display(Name = "Production Order #")]
		public string? ProductionOrderNumber { get; set; }

        //New properties start here down to Navigations!!!!
        [Display(Name ="QI Completed")]
        public bool QIComplete { get; set; }

        [Display(Name = "NCR Raised")]
        public bool NCRRaised { get; set; }

        [Display(Name ="Value")]
        [DataType(DataType.Currency)]
        public double? Value { get; set; }

        //If Date has "Expected" it's from a Kick off Meeting and is a required field in Schedule creation.
        //Dates with name of "Released" or "Returns" in them are NOT required for creation but will be milestones for Gantt creation        
		#region Date Data Captures
        /// <summary>
        /// KickoffMeeting is used as a flag in some views ex. HasValue() for listing active orders
        /// </summary>
		[Display(Name = "Kickoff Meeting")]
        [DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime? KickoffMeeting { get; set; }

        

        [Display(Name = "Expected Approval Drawing")]
        [Required(ErrorMessage = "The Expected Approval drawing date is required")]
        [DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime ApprovalDrawingExpected { get; set; }

        public double? ProgressApprovalDrawing { get; set; }

        [Display(Name = "Approval Drawings Released")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime? ApprovalDrawingReleased { get; set; }

		[Display(Name = "Approval Drawings Returned")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime? ApprovalDrawingReturned { get; set; }


		[Display(Name = "Pre-Order Expected")]
		[Required(ErrorMessage = "The Expected Pre-Order date is required")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime PreOrderExpected { get; set; }

        public double? ProgressPreOrder { get; set; }

		[Display(Name = "Pre-Order Released")]		
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime? PreOrderReleased { get; set; }

        [Display(Name ="Eng Package Expected")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Expected Engineer package date is a required date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime EngineerPackageExpected { get; set; }

        public double?ProgressEngineerPackage {  get; set; }

		[Display(Name = "Eng Package Released")]
        [DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime? EngineerPackageReleased { get; set; }

		[Display(Name = "Purchase order /n Expected")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage ="Expected Purchase Order date is required")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime PurchaseOrderExpected { get; set; }

        public double? ProgressPurchaseOrder { get; set; }   

		[Display(Name = "Purchase Orders Due")]
        [DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime? PurchaseOrderDueDate { get; set; }


		[Display(Name = "Purchase Order Recieved")]
        [DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime? PUrchaseOrderRecieved { get; set; }

		[Display(Name = "RTS Expected")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage ="The Expected RTS date is required")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime ReadinessToShipExpected { get; set; }

        public double? ProgressReadinesstoShip {  get; set; }

        [Display(Name ="Actual RTS")]
        [DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime? ReadinessToShipActual { get; set; }

        #endregion
        
       

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
