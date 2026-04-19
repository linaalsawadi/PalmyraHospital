namespace PalmyraHospital.Application.DTOs.Admin;

public class AdminDashboardViewModel
{
    public int TotalDoctors { get; set; }
    public int ArchivedDoctors { get; set; }
    public int TotalPatients { get; set; }
    public int TotalDepartments { get; set; }
    public int TodayAppointments { get; set; }
}
