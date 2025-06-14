﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace CW_9_s30071.Models;

[Table("PrescriptionMedicament")]
[PrimaryKey(nameof(IdMedicament), nameof(IdPrescription))]
public class PrescriptionMedicament
{
    public int IdMedicament { get; set; }
    public int IdPrescription { get; set; }
    public int? Dose { get; set; }
    [MaxLength(100)]
    public string Details { get; set; }
    
    [ForeignKey(nameof(IdPrescription))]
    public virtual Prescription Prescription { get; set; } = null!;
    
    [ForeignKey(nameof(IdMedicament))]
    public virtual Medicament Medicament { get; set; } = null!;
}