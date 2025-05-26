using CW_9_s30071.Data;
using CW_9_s30071.Exceptions;
using CW_9_s30071.Models;
using CW_9_s30071.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CW_9_s30071.Services;

public interface IDbService
{
    public Task<PrescriptionGetDTO> CreatePrescriptionAsync(PrescriptionCreateDTO prescriptionData);
    public Task<PatientGetDTO> GetPatientDetailsByIdAsync(int id);
}
public class DbService(AppDbContext dbContext) : IDbService
{
    public async Task<PrescriptionGetDTO> CreatePrescriptionAsync(PrescriptionCreateDTO prescriptionData)
{
    var patient = await dbContext.Patients.FindAsync(prescriptionData.IdPatient);
    if (patient == null)
        throw new NotFoundException($"Patient with id: {prescriptionData.IdPatient} not found");
    
    var doctor = await dbContext.Doctors.FindAsync(prescriptionData.IdDoctor);
    if (doctor == null)
        throw new NotFoundException($"Doctor with id: {prescriptionData.IdDoctor} not found");
    
    var prescription = new Prescription
    {
        Date = prescriptionData.Date,
        DueDate = prescriptionData.DueDate,
        IdPatient = prescriptionData.IdPatient,
        IdDoctor = prescriptionData.IdDoctor,
        PrescriptionMedicaments = new List<PrescriptionMedicament>()
    };
    
    if (prescriptionData.Medicaments != null && prescriptionData.Medicaments.Any())
    {
        foreach (var medDto in prescriptionData.Medicaments)
        {
            var medicament = await dbContext.Medicaments.FindAsync(medDto.IdMedicament);
            if (medicament == null)
                throw new NotFoundException($"Medicament with id: {medDto.IdMedicament} not found");

            prescription.PrescriptionMedicaments.Add(new PrescriptionMedicament
            {
                IdMedicament = medicament.IdMedicament,
                Dose = medDto.Dose,
                Details = medDto.Details
            });
        }
    }
    
    dbContext.Prescriptions.Add(prescription);
    await dbContext.SaveChangesAsync();
    
    return new PrescriptionGetDTO
    {
        Id = prescription.IdPrescription,
        Date = prescription.Date,
        DueDate = prescription.DueDate,
        IdDoctor = prescription.IdDoctor,
        IdPatient = prescription.IdPatient,
        PrescriptionMedicaments = prescription.PrescriptionMedicaments.Select(pm => new PrescriptionMedicamentDTO
        {
            IdMedicament = pm.IdMedicament,
            IdPrescription = prescription.IdPrescription,
            Dose = pm.Dose,
            Details = pm.Details,
            Medicament = new MedicamentGetDTO
            {
                IdMedicament = pm.Medicament.IdMedicament,
                Name = pm.Medicament.Name,
                Description = pm.Medicament.Description,
                Type = pm.Medicament.Type
            }
        }).ToList()
    };
}


    public async Task<PatientGetDTO> GetPatientDetailsByIdAsync(int id)
    {
        var result = await dbContext.Patients
            .Where(p => p.IdPatient == id)
            .Select(p => new PatientGetDTO
            {
                Id = p.IdPatient,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Birthdate = p.Birthdate,
                Prescriptions = p.Prescriptions
                    .OrderBy(pr => pr.DueDate)
                    .Select(pr => new PrescriptionGetDTO
                    {
                        Id = pr.IdPrescription,
                        Date = pr.Date,
                        DueDate = pr.DueDate,
                        IdPatient = pr.IdPatient,
                        IdDoctor = pr.IdDoctor,
                        PrescriptionMedicaments = pr.PrescriptionMedicaments
                            .Select(pm => new PrescriptionMedicamentDTO
                            {
                                IdMedicament = pm.IdMedicament,
                                IdPrescription = pm.IdPrescription,
                                Dose = pm.Dose,
                                Details = pm.Details,
                                Medicament = new MedicamentGetDTO
                                {
                                    IdMedicament = pm.Medicament.IdMedicament,
                                    Name = pm.Medicament.Name,
                                    Description = pm.Medicament.Description,
                                    Type = pm.Medicament.Type
                                }
                            }).ToList()
                    }).ToList()
            })
            .FirstOrDefaultAsync();

        return result ?? throw new NotFoundException($"Patient with id: {id} not found");
    }
}