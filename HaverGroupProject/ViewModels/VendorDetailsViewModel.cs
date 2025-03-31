using HaverGroupProject.Models;

namespace HaverGroupProject.ViewModels
{
	public class VendorDetailsViewModel
	{
		public Vendor Vendors { get; set; }
		public int ActiveOrdersCount { get; set; }
		public List<OperationsSchedule> ActiveOrders { get; set; }
	}
}
