using System;

namespace Voenkomat.Models;

public class Visit
{
    public int Id { get; set; }
    public int DudeId { get; set; }
    public DateTime Date { get; set; }
    public string Purpose { get; set; }
    public string Result { get; set; }
}