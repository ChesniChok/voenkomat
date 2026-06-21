using System.Text.Json;
using Voenkomat_Kursach.Models;

namespace Tests;

public class AppSettingsTests
{
    [SetUp]
    public void Setup()
    {
        using (var fs = File.Create("testsSettings.json"))
        {
            var conStr = "server=localhost;user=root;password=root;database=fake_voenkomat";

            Dictionary<string, string> dict = new();
            dict.Add("test", "good");
            dict.Add("no test", "bad");
            
            
            JsonSerializer.Serialize(fs, new AppSettings(conStr, dict));
        }
    }

    [Test]
    public void AppSettingsReadsFile()
    {
        
        var sets = new AppSettings();
        
        sets.Load("testsSettings.json");

        Assert.That(sets.ConnectionString, Is.EqualTo("server=localhost;user=root;password=root;database=fake_voenkomat"));


        Assert.That(sets.Roles.GetValueOrDefault("test"), Is.EqualTo("good"));
        Assert.That(sets.Roles.GetValueOrDefault("no test"), Is.EqualTo("bad"));
        
    }
    
}