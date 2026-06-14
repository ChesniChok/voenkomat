using System;

namespace Voenkomat_Kursach.Models;

public class Record
{
    
    public int Id { get; set; }
    public string Type { get; set; }
    public Employee? Author { get; set; }
    public MedComission? MedComisiion { get; set; }
    public Recruit? Recruit { get; set; }
    public string Content {get; set;}
    public string Description { get; set; }


    public Record()
    {
        Id = -1;
        Type = "";
        Author = new();
        MedComisiion = new();
        Recruit = new();
        Content = "";
        Description = "";
    }
    
    public Record(int id, string type, Employee? author, MedComission? medComisiion, Recruit? recruit, string content, string description)
    {
        Id = id;
        Type = type;
        Author = author;
        MedComisiion = medComisiion;
        Recruit = recruit;
        Content = content;
        Description = description;
    }

}