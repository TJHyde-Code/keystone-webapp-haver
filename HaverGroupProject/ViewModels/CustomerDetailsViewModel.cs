using HaverGroupProject.Models;

namespace HaverGroupProject.ViewModels
{
    public class CustomerDetailsViewModel
    {
        public Customer Customer { get; set; }
        public int ActiveOrdersCount { get; set; }
        public List<OperationsSchedule> ActiveOrders { get; set; }
    }
}
