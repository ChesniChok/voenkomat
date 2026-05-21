using System;

namespace Voenkomat_Kursach.Models;

public class Visit
{
    
    public int Id { get; set; }
    public Recruit Recruit { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly InTime { get; set; }
    public string Goal { get; set; }
    public TimeOnly OutTime { get; set; }


    public Visit()
    {
        Id = -1;
        Recruit = new();
        Date = DateOnly.FromDateTime(DateTime.Now);
        InTime = TimeOnly.FromDateTime(DateTime.Now);
        Goal = "";
        OutTime = TimeOnly.FromDateTime(DateTime.Now);
    }
    
    public Visit(int id, Recruit recruit, DateOnly date, TimeOnly inTime, string goal, TimeOnly outTime)
    {
        Id = id;
        Recruit = recruit;
        Date = date;
        InTime = inTime;
        Goal = goal;
        OutTime = outTime;
    }
    
}