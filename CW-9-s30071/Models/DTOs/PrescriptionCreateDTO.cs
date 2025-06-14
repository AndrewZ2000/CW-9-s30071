﻿using System.ComponentModel.DataAnnotations;

namespace CW_9_s30071.Models.DTOs;

public class PrescriptionCreateDTO
{
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
    [Required]
    public int IdPatient { get; set; }
    [Required]
    public int IdDoctor { get; set; }
    public List<PrescriptionMedicamentCreateDTO> Medicaments { get; set; }
}