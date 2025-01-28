using System.ComponentModel.DataAnnotations;

namespace HaverGroupProject.Models
{
    public class Additions
    {
        //MT-Added primary key definition in order use SeedData
        public int ID { get; set; }

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

        //MT-Fixed typo on "Disassembly"
        [Display(Name = "Disassembly")]
        public bool Disassembly { get; set; }
    }
}
