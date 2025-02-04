using Microsoft.AspNetCore.Mvc.Rendering;

namespace HaverGroupProject.ViewModels
{
    public class MultiStepOperationsScheduleViewModel
    {
        public int ID { get; set; }

        public int SalesOrdNum { get; set; }

        public int ExtSalesOrdNum { get; set; }

        public string? PackageReleaseName { get; set; }

        public DateOnly? KickoffMeeting { get; set; }

        public DateOnly? ReleaseApprovalDrawing { get; set; }

        //Vendor fields
        public string? ProductionOrderNumber { get; set; }

        public DateOnly? PODueDate { get; set; }

        public DateOnly? DeliveryDate { get; set; }

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

        public int? MachineDescriptionID { get; set; }
        public List<SelectListItem>? Machines { get; set; }

        public int? CustomerID { get; set; }
        public List<SelectListItem>? Customers { get; set; }

        public List<SelectListItem>? Vendors { get; set; }
        public string[]? SelectedVendorIDs { get; set; }
    }
}
