using System;
using System.Runtime.InteropServices.JavaScript;

namespace Voenkomat.Models;

public class PassingME
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public int MedicalExaminationId { get; set; }
    public string DoctorsReport { get; set; }
}