using System;

namespace Voenkomat_Kursach.Models;

public class Recruit
{
    
    public int Id { get; set; }
    public string FamilyName { get; set; }
    public string Name { get; set; }
    public string? FatherName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }
    public string Adress { get; set; }
    public string Passport { get; set; }
    public string SNILS { get; set; }
    public string INN { get; set; }


    public Recruit()
    {
        Id = 0;
        FamilyName = "Такоев";
        Name = "Такой";
        FatherName = "Такоевич";
        DateOfBirth = DateOnly.FromDateTime(DateTime.Now);
        PhoneNumber = "88005553535";
        Adress = "где-то далеко";
        Passport = "01 01  123456";
        SNILS = "123456789 01";
        INN = "123456789012";
    }
    
    public Recruit(int id, string familyName, string name, string fatherName, DateOnly dateOfBirth, string phoneNumber, string adress, string passport, string snils, string inn)
    {
        Id = id;
        FamilyName = familyName;
        Name = name;
        FatherName = fatherName;
        DateOfBirth = dateOfBirth;
        PhoneNumber = phoneNumber;
        Adress = adress;
        Passport = passport;
        SNILS = snils;
        INN = inn;
    }
    
}