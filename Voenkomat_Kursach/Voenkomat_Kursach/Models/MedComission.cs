using System;

namespace Voenkomat_Kursach.Models;

public class MedComission
{
    
    public int Id { get; set; }
    public Recruit Recruit { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public bool Ter { get; set; }
    public bool Otor { get; set; }
    public bool Psih { get; set; }
    public bool Nevr { get; set; }
    public bool Hir { get; set; }
    public bool Stom { get; set; }
    public bool Okul { get; set; }
    public DateTimeOffset? EndDate { get; set; }
    public string? Category { get; set; }
    public string? Description { get; set; }


    public MedComission()
    {
        Id = 0;
        Recruit = new();
        StartDate = DateTimeOffset.Now;
        Ter = false;
        Otor = false;
        Psih = false;
        Nevr = false;
        Hir = false;
        Stom = false;
        Okul = false;
        EndDate = null;
        Category = null;
        Description = null;
    }
    
    public MedComission(int id, Recruit recruit, DateTimeOffset startDate, bool ter, bool otor, bool psih, bool nevr, bool hir, bool stom, bool okul, DateTimeOffset? endDate, string category, string? description)
    {
        Id = id;
        Recruit = recruit;
        StartDate = startDate;
        Ter = ter;
        Otor = otor;
        Psih = psih;
        Nevr = nevr;
        Hir = hir;
        Stom = stom;
        Okul = okul;
        EndDate = endDate;
        Category = category;
        Description = description;
    }
    
}