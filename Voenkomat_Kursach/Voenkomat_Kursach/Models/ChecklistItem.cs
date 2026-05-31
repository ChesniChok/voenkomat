namespace Voenkomat_Kursach.Models;

public class ChecklistItem
{
    
    public int Id { get; set; }
    public Job Doctor { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public bool IsChecked { get; set; }//для датагрида
    public string Written { get; set; }//сюда врач оставляет запись


    public ChecklistItem()
    {
        Id = -1;
        Doctor = new();
        Name = "";
        Description = "";
        
        IsChecked = false;
        Written = "";
    }
    
    public ChecklistItem(int id, Job doctor, string name, string description)
    {
        Id = id;
        Doctor = doctor;
        Name = name;
        Description = description;
        
        IsChecked = false;
        Written = "";
    }
    
}