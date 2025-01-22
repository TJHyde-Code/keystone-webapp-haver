namespace HaverGroupProject.Models
{
    public class Vendor
    {
        public int ID { get; set; }

        public string VendorName { get; set; }

        public string VendorAddress { get; set; }

        public string VendorContactName { get; set; }

        public string VendorPhone { get; set; }

        public string VendorEmail { get; set; }

        public ICollection<OperationsSchedule>? OperationsSchedules { get; set; } = new HashSet<OperationsSchedule>();

        

    }
}
