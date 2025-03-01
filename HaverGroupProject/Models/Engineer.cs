using System.ComponentModel.DataAnnotations;

namespace HaverGroupProject.Models
{
    public class Engineer
    {
        public int ID { get; set; }

        #region Summary Properties

        public string EngSummary
        {
            get
            {
                return EngFirstName
                    + (string.IsNullOrEmpty(EngMiddleName) ? " " :
                        (" " + (char?)EngMiddleName[0] + ". ").ToUpper())
                    + EngLastName;
            }
        }

        public string EngInitial
        {
            get
            {
                return (EngFirstName?[0].ToString().ToUpper() ?? "")
                    + (string.IsNullOrEmpty(EngMiddleName) ? "" : " " + EngMiddleName[0].ToString().ToUpper() + ".")
                    + (EngLastName?[0].ToString().ToUpper() ?? "");
            }
        }


        [Display(Name = "Phone")]
        public string PhoneFormatted => "(" + EngPhone?.Substring(0, 3) + ") "
            + EngPhone?.Substring(3, 3) + "-" + EngPhone?[6..];

        #endregion

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "A First Name is required for all Engineers")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only alphabetic characters are allowed")]
        [StringLength(50, ErrorMessage = "Name must be less than 50 Characters in length")]
        public string EngFirstName { get; set; } = "";


        [Display(Name = "Middle Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only alphabetic characters are allowed")]
        [StringLength(50, ErrorMessage = "Name must be less than 50 Characters in length")]
        public string? EngMiddleName { get; set; } = "";


        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "A Last Name is required for all Engineers")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only alphabetic characters are allowed")]
        [StringLength(50, ErrorMessage = "Last Name must be less than 50 Characters in length")]
        public string EngLastName { get; set; } = "";


        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10)]
        public string? EngPhone { get; set; }

        [Required(ErrorMessage = "Email Address is required.")]
        [StringLength(255)]
        [DataType(DataType.EmailAddress)]
        public string? EngEmail { get; set; }

       

        //Navigations
        public ICollection<OperationsSchedule> OperationsSchedules { get; set; } = new HashSet<OperationsSchedule>();

        public ICollection<HaverGantt> HaverGantts { get; set; } = new HashSet<HaverGantt>();
    }
}
