namespace HaverGroupProject.Models
{
    public class Customer
    {
        public int ID { get; set; }

        public string CustomerName { get; set; }

        public DateOnly? ReleaseDate { get; set; }

        public string? CustomerAddress { get; set; }

        public string? CustomerContactName { get; set; }

        public string? CustomerEmail { get; set; }

        public ICollection<OperationsSchedule>? OperationsSchedules { get; set; }

    }
}
