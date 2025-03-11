using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HaverGroupProject.ViewModels
{
    public class MultiStepOperationsScheduleViewModel
    {
        public int ID { get; set; }

        [Display (Name ="Sales Order #")]
        public int SalesOrdNum { get; set; }

        [Display(Name = "External Sales Order #")]
        public int ExtSalesOrdNum { get; set; }

        [Display(Name = "Package Release Name")]
        public string? PackageReleaseName { get; set; }

        //MT added format so no minutes/seconds gets displayed
        [Display (Name = "Kick-off Meeting Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? KickoffMeeting { get; set; }

        [Display(Name = "Release Approval Drawing Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ReleaseApprovalDrawing { get; set; }

        //Vendor fields
        [Display(Name = "Purchase Order Number")]
        public string? ProductionOrderNumber { get; set; }

        [Display(Name = "PO Due Date")]
        public DateTime? PODueDate { get; set; }

        [Display(Name = "Delivery Date")]
        public DateTime? DeliveryDate { get; set; }

        //Properties for MachineDescription creation. 
        public string DescriptionSummary
        {
            get
            {
                return Class + " " + Size + " " + "-" + Deck;
            }
        }

        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; } = "";

        //This may need to be adjusted later pending their requirments.
        [Display(Name = "Size")]
        [StringLength(6, ErrorMessage = " String length can't be longer than 6 characters long. ' inclusive.")]
        [RegularExpression(@"^\d+'x\d+'$", ErrorMessage = "Size must be supplied in the format of 6'x20'")]
        public string Size { get; set; } = "";

        [Display(Name = "Class")]
        [RegularExpression(@"^[A-Z]-\d{3}$", ErrorMessage = "Class must be in the format of X-###. For example T-330")]
        //[Required(ErrorMessage = "All Machine descriptions must be supplied a class")]
        [StringLength(5, ErrorMessage = "Machine Class cannot be longer than 5 characters long. This includes '-' ")]
        public string Class { get; set; } = "";

        [Display(Name = "Deck")]
        //[Required(ErrorMessage = "All Machine descriptions must be supplied with a number of decks.")]
        [StringLength(2, ErrorMessage = "Machine deck must be 2 characters in length.")]
        [RegularExpression(@"^\d+D$", ErrorMessage = "Deck must be in the given as #D, for example 1D")]
        public string Deck { get; set; } = "";

        //Bool fields for machine description
        public bool NamePlateOrdered { get; set; } = false;
        public bool NameplateRecieved { get; set; } = false;
        public bool InstalledMedia { get; set; } = false;
        public bool SparePartsSpareMedia { get; set; } = false;
        public bool BaseFrame { get; set; } = false;
        public bool AirSeal { get; set; } = false;
        public bool CoatingLining { get; set; } = false;
        public bool Disassembly { get; set; } = false;

        //Notes fields
        public string? PreOrder { get; set; }
        public string? Scope { get; set; }
        public string? BudgetAssembHrs { get; set; }
        public int? ActualAssembHours { get; set; }
        public int? ActualReworkHours { get; set; }
        public string? OtherComments { get; set; }

        [Display(Name = "Machine Description")]
        public int? MachineDescriptionID { get; set; }
        public List<SelectListItem>? Machines { get; set; }

        [Display(Name = "Customer Name")]
        public int? CustomerID { get; set; }
        public List<SelectListItem>? Customers { get; set; }

        [Display(Name = "Vendors")]
        public List<SelectListItem>? Vendors { get; set; }
        public string[]? SelectedVendorIDs { get; set; }

        [Display(Name = "Machines")]
        public List<SelectListItem>? SelectMachines { get; set; }
        public string[]? SelectedMachineID { get; set; }
    }
}
