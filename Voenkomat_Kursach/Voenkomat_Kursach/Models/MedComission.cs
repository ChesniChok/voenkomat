using System;

namespace Voenkomat_Kursach.Models;

public class MedComission
{
    
    public int Id { get; set; }
    public Recruit Recruit { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string? Category { get; set; }
    public string? Description { get; set; }


    public MedComission()
    {
        Id = 0;
        Recruit = new();
        StartDate = DateOnly.FromDateTime(DateTime.Now);
        EndDate = null;
        Category = null;
        Description = null;
    }
    
    public MedComission(int id, Recruit recruit, DateOnly startDate, DateOnly? endDate, string category, string? description)
    {
        Id = id;
        Recruit = recruit;
        StartDate = startDate;
        EndDate = endDate;
        Category = category;
        Description = description;
    }
    
}