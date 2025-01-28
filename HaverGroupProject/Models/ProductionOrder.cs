using System.ComponentModel.DataAnnotations;

namespace HaverGroupProject.Models
{
    public class ProductionOrder
    {
        public int ID { get; set; }

        public string? ProductionOrderNumber { get; set; }

        public OperationsSchedule? OperationsScheduleID { get; set; }

        public Vendor? Vendor { get; set; }
    }
}
