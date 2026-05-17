using System;

namespace Voenkomat.Models;

public class MedicalExamination
{
    public int Id { get; set; }
    public int DudeId { get; set; }
    public DateTime Date { get; set; }
    public string Conclusion { get; set; }
    
}