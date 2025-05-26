namespace CW_9_s30071.Models.DTOs;

public class PatientGetDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime Birthdate { get; set; }
    public ICollection<PrescriptionGetDTO> Prescriptions { get; set; } = null!;
}