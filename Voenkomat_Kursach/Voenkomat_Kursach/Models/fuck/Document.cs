namespace Voenkomat.Models;

public class Document
{
    public int Id { get; set; }
    public string Type { get; set; }
    public int Number { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int DudeId { get; set; }
    public int EmployeeId { get; set; }
}