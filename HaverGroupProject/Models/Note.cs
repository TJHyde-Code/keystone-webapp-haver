using System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Client;

namespace HaverGroupProject.Models
{
    public class Note
    {
        public int ID { get; set; }

        [Display(Name ="Pre-Order")]
        [StringLength(200, ErrorMessage ="Pre-Order notes cannot exceed 200 characters in length")]
        public string PreOrder { get; set; } = "";

        [Display(Name="Scope")]
        [StringLength(100, ErrorMessage = "Scope note cannot exceed 100 characters in length.")]
        public string Scope { get; set; } = "";

        //Leaving as string for now as they have additional notes/variations on the current schedule.
        [Display(Name = "Budget Assembly Hrs:")]
        public string BudgetAssembHrs { get; set; } = "";

        [Display(Name = "Actual Assembly Hrs:")]
        public int ActualAssembHours { get; set; }

        [Display(Name = "Actual Rework Hrs:")]        
        public int ActualReworkHours { get; set; }

        [Display(Name = "Other Comments")]
        [StringLength(500, ErrorMessage = "Extra comments must be under 250 characters")]
        [DataType(DataType.MultilineText)]
        public string OtherComments { get; set; } = "";

        [ScaffoldColumn(false)]
        [Timestamp]
        public Byte[]? RowVersion { get; set; }//Added for concurrency

        public int OperationsScheduleID { get; set; }
        public OperationsSchedule? OperationsSchedule { get; set; }
    }
}
