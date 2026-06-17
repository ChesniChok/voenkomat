using System;

namespace Voenkomat_Kursach.Models;

public class Visit
{
    
    public int Id { get; set; }
    public MedComission MedComission { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly InTime { get; set; }
    public string Goal { get; set; }
    public TimeOnly OutTime { get; set; }


    public Visit()
    {
        Id = 0;
        MedComission = new();
        Date = DateOnly.FromDateTime(DateTime.Now);
        InTime = TimeOnly.FromDateTime(DateTime.Now);
        Goal = "";
        OutTime = TimeOnly.FromDateTime(DateTime.Now);
    }
    
    public Visit(int id, MedComission medComission, DateOnly date, TimeOnly inTime, string goal, TimeOnly outTime)
    {
        Id = id;
        MedComission = medComission;
        Date = date;
        InTime = inTime;
        Goal = goal;
        OutTime = outTime;
    }
    
}