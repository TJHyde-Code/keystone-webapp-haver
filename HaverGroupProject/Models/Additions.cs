using System.ComponentModel.DataAnnotations;

namespace HaverGroupProject.Models
{
    public class Additions
    {
        [Display(Name = "Media")]
        public bool InstalledMedia { get; set; }

        [Display(Name = "Spare Parts / Media")]
        public bool SparePartsSpareMedia { get; set; }

        [Display(Name = "Base")]
        public bool BaseFrame { get; set; }

        [Display(Name = "Air Seal")]
        public bool AirSeal { get; set; }

        [Display(Name = "Coating / Lining")]
        public bool CoatingLining { get; set; }

        [Display(Name = "Disassembly")]
        public bool Disassebmly { get; set; }
    }
}
