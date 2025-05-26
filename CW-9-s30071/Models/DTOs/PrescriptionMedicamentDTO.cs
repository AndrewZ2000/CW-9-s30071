namespace CW_9_s30071.Models.DTOs;

public class PrescriptionMedicamentDTO
{
    public int IdMedicament { get; set; }
    public int IdPrescription { get; set; }
    public int? Dose { get; set; }
    public string Details { get; set; }
    public MedicamentGetDTO Medicament { get; set; } = null!;
}