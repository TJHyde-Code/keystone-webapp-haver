namespace HaverGroupProject.Models
{
    public class OperationsScheduleMachine
    {
        public int OperationsScheduleID { get; set; }
        public OperationsSchedule? OperationsSchedule { get; set; }

        public int MachineDescriptionID { get; set; }
        public MachineDescription? MachineDescription { get; set; }
    }
}
