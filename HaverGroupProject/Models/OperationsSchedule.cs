using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Security.Permissions;

namespace HaverGroupProject.Models
{
    public class OperationsSchedule : IValidatableObject
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


        #region Date Validation
        //First, simple validation to ensure dates are not chosen from the past
        // The .GetValueOrDefault is because the dates can be nullable (at least for now)
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (KickoffMeeting.GetValueOrDefault() < DateTime.Now)
            {
                yield return new ValidationResult("Kick off meeting date must not be set in the past", ["KickoffMeeting"]);
            }

            if (ApprovalDrawingExpected < DateTime.Now)
            {
                yield return new ValidationResult("Approval drawing expected date must not be set in the past", ["ApprovalDrawingExpected"]);
            }

            if (ApprovalDrawingReleased.GetValueOrDefault() < DateTime.Now)
            {
                yield return new ValidationResult("Approval drawing released date must not be set in the past", ["ApprovalDrawingReleased"]);
            }

            if (ApprovalDrawingReturned.GetValueOrDefault() < DateTime.Now)
            {
                yield return new ValidationResult("Approval drawing returned date must not be set in the past", ["ApprovalDrawingReturned"]);
            }

            if (PreOrderExpected < DateTime.Now)
            {
                yield return new ValidationResult("Pre-order expected date must not be set in the past", ["PreOrderExpected"]);
            }

            if (PreOrderReleased.GetValueOrDefault() < DateTime.Now)
            {
                yield return new ValidationResult("Pre-order released date must not be set in the past", ["PreOrderReleased"]);
            }

            if (EngineerPackageExpected < DateTime.Now)
            {
                yield return new ValidationResult("Engineer package expected date must not be set in the past", ["EngineerPackageExpected"]);
            }

            if (EngineerPackageReleased.GetValueOrDefault() < DateTime.Now)
            {
                yield return new ValidationResult("Engineer package released date must not be set in the past", ["EngineerPackageReleased"]);
            }

            if (PurchaseOrderExpected < DateTime.Now)
            {
                yield return new ValidationResult("Purchase orders expected date must not be set in the past", ["PurchaseOrderExpected"]);
            }

            if (PurchaseOrderDueDate.GetValueOrDefault() < DateTime.Now)
            {
                yield return new ValidationResult("Purchase orders due date must not be set in the past", ["PurchaseOrderDueDate"]);
            }

            if (PUrchaseOrderRecieved < DateTime.Now)
            {
                yield return new ValidationResult("Purchase orders received date must not be set in the past", ["PurchaseOrderRecieved"]);
            }

            if (ReadinessToShipExpected < DateTime.Now)
            {
                yield return new ValidationResult("Readiness to ship expected date must not be set in the past", ["ReadinessToShipExpected"]);
            }

            if (ReadinessToShipActual.GetValueOrDefault() < DateTime.Now)
            {
                yield return new ValidationResult("Readiness to ship actual date must not be set in the past", ["ReadinessToShipActual"]);
            }
            //Validation chain for Milestone dates.
            if (KickoffMeeting.HasValue && KickoffMeeting.Value > ApprovalDrawingExpected)
            {
                yield return new ValidationResult("Approval Drawing Expected must be after Kickoff Meeting", ["ApprovalDrawingExpected"]);
            }
            else if (ApprovalDrawingReleased.HasValue && ApprovalDrawingExpected > ApprovalDrawingReleased.Value)
            {
                yield return new ValidationResult("Approval Drawing Released must be after Approval Drawing Expected", ["ApprovalDrawingReleased"]);
            }
            else if (ApprovalDrawingReturned.HasValue && ApprovalDrawingReleased.HasValue &&
                     ApprovalDrawingReleased.Value > ApprovalDrawingReturned.Value)
            {
                yield return new ValidationResult("Approval Drawing Returned must be after Approval Drawing Released", ["ApprovalDrawingReturned"]);
            }
            else if (PreOrderExpected < ApprovalDrawingExpected)
            {
                yield return new ValidationResult("Pre-Order Expected must be after Approval Drawing Expected", ["PreOrderExpected"]);
            }
            else if (PreOrderReleased.HasValue && PreOrderExpected > PreOrderReleased.Value)
            {
                yield return new ValidationResult("Pre-Order Released must be after Pre-Order Expected", ["PreOrderReleased"]);
            }
            else if (EngineerPackageExpected < PreOrderExpected)
            {
                yield return new ValidationResult("Engineer Package Expected must be after Pre-Order Expected", ["EngineerPackageExpected"]);
            }
            else if (EngineerPackageReleased.HasValue && EngineerPackageExpected > EngineerPackageReleased.Value)
            {
                yield return new ValidationResult("Engineer Package Released must be after Engineer Package Expected", ["EngineerPackageReleased"]);
            }
            else if (PurchaseOrderExpected < EngineerPackageExpected)
            {
                yield return new ValidationResult("Purchase Order Expected must be after Engineer Package Expected", ["PurchaseOrderExpected"]);
            }
            else if (PurchaseOrderDueDate.HasValue && PurchaseOrderExpected > PurchaseOrderDueDate.Value)
            {
                yield return new ValidationResult("Purchase Order Due Date must be after Purchase Order Expected", ["PurchaseOrderDueDate"]);
            }
            else if (PUrchaseOrderRecieved.HasValue && PurchaseOrderDueDate.HasValue &&
                     PurchaseOrderDueDate.Value > PUrchaseOrderRecieved.Value)
            {
                yield return new ValidationResult("Purchase Order Received must be after Purchase Order Due Date", ["PurchaseOrderRecieved"]);
            }
            else if (ReadinessToShipExpected < PurchaseOrderExpected)
            {
                yield return new ValidationResult("RTS Expected must be after Purchase Order Expected", ["ReadinessToShipExpected"]);
            }
            else if (ReadinessToShipActual.HasValue && ReadinessToShipExpected > ReadinessToShipActual.Value)
            {
                yield return new ValidationResult("Actual RTS must be after RTS Expected", ["ReadinessToShipActual"]);
            }


        }

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
