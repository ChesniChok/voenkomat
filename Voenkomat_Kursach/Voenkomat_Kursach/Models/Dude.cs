using System;

namespace Voenkomat_Kursach.Models;

public class Dude
{
    
    public int Id { get; set; }
    public string FamilyName { get; set; }
    public string Name { get; set; }
    public string FatherName { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string Adress { get; set; }
    public string Passport { get; set; }
    public string SNILS { get; set; }
    public string INN { get; set; }


    public Dude()
    {
        Id = -1;
        FamilyName = "зубенко";
        Name = "михаил";
        FatherName = "петрович";
        DateOfBirth = DateTimeOffset.Now;
        PhoneNumber = "88005553535";
        Adress = "чуркин/65/5/27";
        Passport = "26 05  123456";
        SNILS = "00000000000";
        INN = "9999999999999999999999999999999999999";
    }
    
    public Dude(int id, string familyName, string name, string fatherName, DateTimeOffset dateOfBirth, string phoneNumber, string adress, string passport, string snils, string inn)
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