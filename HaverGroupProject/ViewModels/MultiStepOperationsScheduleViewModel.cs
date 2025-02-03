using Microsoft.AspNetCore.Mvc.Rendering;

namespace HaverGroupProject.ViewModels
{
    public class MultiStepOperationsScheduleViewModel
    {
        public int ID { get; set; }

        public int SalesOrdNum { get; set; }

        public int? MachineDescriptionID { get; set; }
        public List<SelectListItem>? Machines { get; set; }

        public int? CustomerID { get; set; }
        public List<SelectListItem>? Customers { get; set; }

        public List<SelectListItem>? Vendors { get; set; }
        public string[]? SelectedVendorIDs { get; set; }
    }
}
