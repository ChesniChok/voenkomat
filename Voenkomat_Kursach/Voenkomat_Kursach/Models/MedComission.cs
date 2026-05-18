using System;

namespace Voenkomat_Kursach.Models;

public class MedComission
{
    
    public int Id { get; set; }
    public Dude Dude { get; set; }
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


    public MedComission()
    {
        Id = -1;
        Dude = new();
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
    }
    
    public MedComission(int id, Dude dude, DateTimeOffset startDate, bool ter, bool otor, bool psih, bool nevr, bool hir, bool stom, bool okul, DateTimeOffset? endDate, string category)
    {
        Id = id;
        Dude = dude;
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
    }
    
}