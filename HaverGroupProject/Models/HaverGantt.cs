using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaverGroupProject.Models
{
    public class HaverGantt
    {
        public int ID { get; set; }

        #region Summaries
        public string CustomerTask
        {
            get
            {
                return Customer?.CustomerName + "(" + Vendor?.VendorName + ")";   
            }
        }

        #endregion

        [Display(Name = "PO Number")]
        [Required(ErrorMessage = "A Purchase order number must be given")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "The PO number must be exactly 8 characters.")]
        public string PurchaseOrderNum { get; set; } = "";

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Start date is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Promise Date")]
        [Required(ErrorMessage = "Promise Date is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PromiseDate { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name ="Approved Dwg Rec'd")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? ApprvDwgRecvd { get; set; }

        [Required(ErrorMessage = "You must enter progress.")]
        [Range(0, 1, ErrorMessage = "Progress must be between 0 and 1.")]
        public decimal Progress { get; set; }

        [Display(Name = "Progress (%)")]
        public int ProgressPercentage => (int)(Progress * 100);


        [Display(Name = "Other Comments")]
        [StringLength(500, ErrorMessage = "Extra comments must be under 500 characters")]
        [DataType(DataType.MultilineText)]
        public string? GanttNotes { get; set; }


        //Navigations

        [Display(Name = "Machine Desc.")]
        [ForeignKey("MachineDescription")]
        public int? MachineDescriptionID { get; set; }
        public MachineDescription? MachineDescription { get; set; }

        [Display(Name = "Customer")]
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }
        public Customer? Customer { get; set; }

        [Display(Name = "Engineer")]
        [ForeignKey("Engineer")]
        public int? EngineerID { get; set; }
        public Engineer? Engineer { get; set; }

        [Display(Name = "Vendor")]
        [ForeignKey("Vendor")]
        public int? VendorID { get; set; }
        public Vendor? Vendor { get; set; }

        //public ICollection<HaverGantt> HaverGantts { get; set; } = new HashSet<HaverGantt>();
        

    }
}
