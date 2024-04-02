namespace FinalProjectNew.Models
{
    public class LeaveRequest
    {
        public int Id { get; set; }
        public DateTime LeaveDate { get; set; }   // New column for actual day of leave - duration half (4 hours) or full (eight hours)
        //public DateTime StartDate { get; set; } // Removed - create an entry for each day off
        //public DateTime EndDate { get; set; }   // Removed - create an entry for each day off
        public int EmployeeID { get; set; }
        public LeaveType LeaveType { get; set; }
        public DurationType DurationType { get; set; }
        public bool Cancelled { get; set; }
        public string? RequestComments { get; set; }
    }
}