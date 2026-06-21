using System.Text.Json;
using MySqlConnector;
using Voenkomat_Kursach.DB;
using Voenkomat_Kursach.Models;

namespace Tests.DatabaseTests;

[TestFixture]
public class DatabaseTests
{

    private AppSettings _sets;
    
    [SetUp]
    public void Setup()
    {
        
        var conStr = "server=localhost;user=root;password=root;database=fake_voenkomat";
        Dictionary<string, string> dict = new();
        
        _sets = new(conStr, dict);

        using (MySqlConnection con = new(conStr))
        {
            con.Open();
            using (var cmd = new MySqlCommand("delete from visits", con))
            {
                
                cmd.ExecuteNonQuery();
                
                cmd.CommandText = "delete from records";
                cmd.ExecuteNonQuery();
                
                cmd.CommandText = "delete from medcomissions";
                cmd.ExecuteNonQuery();
                
                cmd.CommandText = "delete from users";
                cmd.ExecuteNonQuery();
                
                cmd.CommandText = "delete from employees";
                cmd.ExecuteNonQuery();
                
                cmd.CommandText = "delete from checkitems";
                cmd.ExecuteNonQuery();
                
                cmd.CommandText = "delete from jobs";
                cmd.ExecuteNonQuery();
                
                cmd.CommandText = "delete from roles";
                cmd.ExecuteNonQuery();
                
                cmd.CommandText = "delete from recruits";
                cmd.ExecuteNonQuery();
                
                
                cmd.CommandText = "insert into jobs values(999, \'testJob\')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "insert into checkitems values(888, 999, \'checkName\', \'checkDesc\')";
                cmd.ExecuteNonQuery();
                
                cmd.CommandText = "insert into jobs values(0, \'пусто\');";
                for (int i = 0; i < 13; i++) cmd.ExecuteNonQuery();


                cmd.CommandText = "insert into recruits values(777, \'fam\', \'nam\', \'fath\', \'2026-01-01\', \'pho\', \'adr\', \'pas\', \'sni\', \'inn\')";
                cmd.ExecuteNonQuery();
                
                cmd.CommandText = "insert into medcomissions values(666, 777, \'2026-01-01\', \'2026-01-01\', \'cat\', \'des\')";
                cmd.ExecuteNonQuery();
                
                cmd.CommandText = "insert into visits values(0, 666, \'2026-01-01\', \'00:00\', \'goa\', \'23:50\')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "insert into visits values(0, 666, @today, \'00:00\', \'goa\', \'23:50\')";
                cmd.Parameters.AddWithValue("@today", DateOnly.FromDateTime(DateTime.Now));
                cmd.ExecuteNonQuery();


            }
        }

    }

    [Test]
    public void RepoGetsSettings()
    {

        var jr = new JobRepository(_sets);

        var reposConnectionString = jr.GetConnection().ConnectionString;
        
        Assert.That(reposConnectionString, Is.EqualTo("server=localhost;user=root;password=root;database=fake_voenkomat"));
        
    }

    [Test]
    public void RepoSurvivesDoubleOpenConnection()
    {
        
        var rr = new RoleRepository(_sets);

        if (rr.OpenConnection())
        {
            rr.OpenConnection();
            
            Assert.Pass();
            return;
        }
        Assert.Fail();
        
    }

    [Test]
    public void RepoSurvivesCloseClosedConnection()
    {
        
        var rr = new RecruitRepository(_sets);

        if (rr.CloseConnection())
        {
            Assert.Pass();
            return;
        }
        Assert.Fail();
        
    }
    
    [Test]
    public void RepoAddWorks()
    {
        
        var rr = new RoleRepository(_sets);

        try
        {
            rr.Add(new());
            rr.Add(new());
            rr.Add(new());
            rr.Add(new());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Assert.Fail();
        }
        
        Assert.Pass();
        
    }

    [Test]
    public void RepoUpdateWorks()
    {
        
        var rr = new RoleRepository(_sets);

        var role = new Role(5, "role", false);
        rr.Add(role);

        role.Name = "rolerolerole";
        
        rr.Update(role);
        
        Assert.That(rr.GetById(5).Name, Is.EqualTo("rolerolerole"));
        
    }

    [Test]
    public void RepoDeleteWorks()
    {
        
        var rr = new RoleRepository(_sets);

        var role = new Role(5, "role", false);
        rr.Add(role);
        
        rr.Delete(role);
        
        Assert.That(rr.GetAll().Count, Is.EqualTo(0));
        
    }

    [Test]
    public void CheckRepoGetsNestedJob()
    {
        
        var jr = new JobRepository(_sets);
        var cr = new ChecklistItemRepository(_sets, jr);

        var checkitem = cr.GetById(888);
        
        Assert.That(checkitem, Is.Not.Null);
        Assert.That(checkitem.Id, Is.EqualTo(888));
        Assert.That(checkitem.Doctor.Id, Is.EqualTo(999));

    }

    [Test]
    public void JobRepoPagination()
    {
        
        var jr = new JobRepository(_sets);

        var jobs = jr.GetPage(0, 10);
        var allJobs = jr.GetAll();
        
        Assert.That(jobs.Count, Is.LessThan(allJobs.Count));

    }

    [Test]
    public void VisitRepoGetsToday()
    {
        
        var rr = new RecruitRepository(_sets);
        var mr = new MedComissionRepository(_sets, rr);
        var vr = new VisitRepository(_sets, mr);
        
        Assert.That(vr.GetPageToday(0, 10).Count, Is.EqualTo(1));
        
    }
    
}