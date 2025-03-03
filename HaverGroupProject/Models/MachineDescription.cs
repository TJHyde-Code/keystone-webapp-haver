using System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Client;

namespace HaverGroupProject.Models
{
    public class MachineDescription
    {
        
        public int ID { get; set; }

        //Summary for Display. Class - Size - Deck.
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
        [Required(ErrorMessage = "All Machine descriptions must be supplied a class")]
        [StringLength(5,ErrorMessage ="Machine Class cannot be longer than 5 characters long. This includes '-' ")]
        public string Class { get; set; } = "";

        [Display(Name = "Deck")]
        [Required(ErrorMessage ="All Machine descriptions must be supplied with a number of decks.")]
        [StringLength(2, ErrorMessage ="Machine deck must be 2 characters in length.")]
        [RegularExpression(@"^\d+D$", ErrorMessage = "Deck must be in the given as #D, for example 1D")]
        public string Deck { get; set; } = "";

        [Display(Name = "Nameplate Ord.")] 
        public bool NamePlateOrdered { get; set; } 


        [Display(Name = "Nameplate Rcvd.")]
        public bool NameplateRecieved { get; set; }

        [Display(Name = "Media")]
        public bool InstalledMedia { get; set; }

        [Display(Name = "Spare Parts / Media")]
        public bool SparePartsSpareMedia { get; set; }

        [Display(Name = "Spare Parts")]
        public bool SpareParts { get; set; }

        [Display(Name = "Base")]
        public bool BaseFrame { get; set; }

        [Display(Name = "Air Seal")]
        public bool AirSeal { get; set; }

        [Display(Name = "Coating / Lining")]
        public bool CoatingLining { get; set; }

        //MT-Fixed typo on "Disassembly"
        [Display(Name = "Disassembly")]
        public bool Disassembly { get; set; }
        //Navigations. 
        public ICollection<OperationsSchedule> OperationsSchedules { get; set; } = new HashSet<OperationsSchedule>();

        public ICollection<HaverGantt> HaverGantts { get; set; } = new HashSet<HaverGantt>();
    }
}
