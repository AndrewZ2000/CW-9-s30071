namespace CW_9_s30071.Models.DTOs;

public class PrescriptionGetDTO
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public int IdPatient { get; set; }
    public int IdDoctor { get; set; }
    public ICollection<PrescriptionMedicamentDTO> PrescriptionMedicaments { get; set; } = null!;
}