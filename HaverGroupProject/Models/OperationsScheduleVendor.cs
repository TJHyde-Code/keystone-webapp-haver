namespace HaverGroupProject.Models
{
    public class OperationsScheduleVendor
    {
        public int OperationsScheduleID { get; set; }
        public OperationsSchedule? OperationsSchedule { get; set; }

        public int VendorID { get; set; }
        public Vendor? Vendor { get; set; }
    }
}
