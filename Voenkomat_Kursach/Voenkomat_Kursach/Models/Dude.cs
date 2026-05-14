using System;

namespace Voenkomat.Models;

public class Dude
{
    public int Id {get; set;}
    public string SerName {get; set; }
    public string Name {get; set;}
    public string SecondName {get; set;}
    public DateTime Birthday { get; set; }
    public int PhoneNumber { get; set; }
    public string Address { get; set; }
    public int SeriesAndNumber {get; set; }
    public int SNILS { get; set; }
    public int INN { get; set; }
    public bool Gender { get; set; }
}