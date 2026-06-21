using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.DB;

public class RecruitRepository : BaseRepository<Recruit>
{
    public RecruitRepository(AppSettings appSettings) : base(appSettings)
    {
        
    }
    public List<Recruit> GetAll()
        {
            List<Recruit> recruits = new List<Recruit>();
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM recruits";
                using (var mc = new MySqlCommand(sql, _connection))
                using (var dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        recruits.Add(new Recruit(
                            dr.GetInt32("Id"),
                            dr.GetString("FamilyName"),
                            dr.GetString("Name"),
                            dr.GetString("FatherName"),
                            dr.GetDateOnly("DateOfBirth"),
                            dr.GetString("PhoneNumber"),
                            dr.GetString("Adress"),
                            dr.GetString("Passport"),
                            dr.GetString("SNILS"),
                            dr.GetString("INN")
                        ));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
            }
            finally
            {
                CloseConnection();
            }
            return recruits;
        }
    
    public List<Recruit> GetAll(string search)
    {
        List<Recruit> recruits = new List<Recruit>();
        try
        {
            _connection.Open();
            string sql = "SELECT * FROM recruits where Name like @name or FatherName like @fath or FamilyName like @fname";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@name", $"%{search}%");
                mc.Parameters.AddWithValue("@fath", $"%{search}%");
                mc.Parameters.AddWithValue("@fname", $"%{search}%");
                using (var dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        recruits.Add(new Recruit(
                            dr.GetInt32("Id"),
                            dr.GetString("FamilyName"),
                            dr.GetString("Name"),
                            dr.GetString("FatherName"),
                            dr.GetDateOnly("DateOfBirth"),
                            dr.GetString("PhoneNumber"),
                            dr.GetString("Adress"),
                            dr.GetString("Passport"),
                            dr.GetString("SNILS"),
                            dr.GetString("INN")
                        ));
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
                
        }
        finally
        {
            CloseConnection();
        }
        return recruits;
    }
    
    public List<Recruit> GetAllToday(string search)
    {
        List<Recruit> recruits = new List<Recruit>();
        try
        {
            OpenConnection();
            string sql = "select distinct \n\tr.*\nfrom\n\tvisits v \n\tjoin \n\tmedcomissions m\n\ton\n\t\tv.Medcomission_Id = m.Id \n\tjoin\n\trecruits r\n\ton\n\t\tm.Recruit_Id = r.Id\nwhere \n\tv.Date = @date\n\tand \n\tv.OutTime is null \n\tand \t\n\tm.EndDate is null\n\tand\n\t(Name like @name or FatherName like @fath or FamilyName like @fname)\norder by \n\tm.StartDate \n;";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@name", $"%{search}%");
                mc.Parameters.AddWithValue("@fath", $"%{search}%");
                mc.Parameters.AddWithValue("@fname", $"%{search}%");
                mc.Parameters.AddWithValue("@date", DateOnly.FromDateTime(DateTime.Now));
                using (var dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        recruits.Add(new Recruit(
                            dr.GetInt32("Id"),
                            dr.GetString("FamilyName"),
                            dr.GetString("Name"),
                            dr.GetString("FatherName"),
                            dr.GetDateOnly("DateOfBirth"),
                            dr.GetString("PhoneNumber"),
                            dr.GetString("Adress"),
                            dr.GetString("Passport"),
                            dr.GetString("SNILS"),
                            dr.GetString("INN")
                        ));
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
                
        }
        finally
        {
            CloseConnection();
        }
        return recruits;
    }
    
        public List<Recruit> GetPage(int offset, int limit)
        {
            List<Recruit> roles = new();
            try
            {
                OpenConnection();
                string sql = "select * from recruits limit @limit offset @offset";
                using (var mc = new MySqlCommand(sql, _connection))
                {
                    mc.Parameters.AddWithValue("@limit", limit);
                    mc.Parameters.AddWithValue("@offset", offset);
                    using (var dr = mc.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            roles.Add(new Recruit
                            (
                                dr.GetInt32("Id"),
                                dr.GetString("FamilyName"),
                                dr.GetString("Name"),
                                dr.GetString("FatherName"),
                                dr.GetDateOnly("DateOfBirth"),
                                dr.GetString("PhoneNumber"),
                                dr.GetString("Adress"),
                                dr.GetString("Passport"),
                                dr.GetString("SNILS"),
                                dr.GetString("INN")
                            ));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                CloseConnection();
                Console.WriteLine(e);
                    
            }
            return roles;
        }
        
        public List<Recruit> GetPage(int offset, int limit, string search)
        {
            List<Recruit> roles = new();
            try
            {
                OpenConnection();
                string sql = "select * from recruits where Name like @name or FatherName like @fath or FamilyName like @fname limit 10 offset 0";
                using (var mc = new MySqlCommand(sql, _connection))
                {
                    mc.Parameters.AddWithValue("@limit", limit);
                    mc.Parameters.AddWithValue("@offset", offset);
                    mc.Parameters.AddWithValue("@name", $"%{search}%");
                    mc.Parameters.AddWithValue("@fath", $"%{search}%");
                    mc.Parameters.AddWithValue("@fname", $"%{search}%");
                    using (var dr = mc.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            roles.Add(new Recruit
                            (
                                dr.GetInt32("Id"),
                                dr.GetString("FamilyName"),
                                dr.GetString("Name"),
                                dr.GetString("FatherName"),
                                dr.GetDateOnly("DateOfBirth"),
                                dr.GetString("PhoneNumber"),
                                dr.GetString("Adress"),
                                dr.GetString("Passport"),
                                dr.GetString("SNILS"),
                                dr.GetString("INN")
                            ));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                CloseConnection();
                Console.WriteLine(e);
                    
            }
            return roles;
        }

        public Recruit GetById(int id)
        {
            Recruit recruit = new Recruit();
            try
            {
                OpenConnection();
                string sql = "SELECT * FROM recruits WHERE id = @id";
                using (var mc = new MySqlCommand(sql, _connection))
                {
                    mc.Parameters.AddWithValue("@Id", id);
                    using (var dr = mc.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            recruit = new Recruit(
                            
                                dr.GetInt32("Id"),
                                dr.GetString("FamilyName"),
                                dr.GetString("Name"),
                                dr.GetString("FatherName"),
                                dr.GetDateOnly("DateOfBirth"),
                                dr.GetString("PhoneNumber"),
                                dr.GetString("Adress"),
                                dr.GetString("Passport"),
                                dr.GetString("SNILS"),
                                dr.GetString("INN")
                            );
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
            }
            finally
            {
                CloseConnection();
            }
            return recruit;
        }
        
    public void Add(Recruit r)
    {
        try
        {
            OpenConnection();
            string sql = "insert into recruits values(0, @fname, @name, @fath, @bday, @number, @adres, @pass, @snils, @inn)";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@fname", r.FamilyName);
                mc.Parameters.AddWithValue("@name", r.Name);
                mc.Parameters.AddWithValue("@fath", r.FatherName);
                mc.Parameters.AddWithValue("@bday", r.DateOfBirth);
                mc.Parameters.AddWithValue("@number", r.PhoneNumber);
                mc.Parameters.AddWithValue("@adres", r.Adress);
                mc.Parameters.AddWithValue("@pass", r.Passport);
                mc.Parameters.AddWithValue("@snils", r.SNILS);
                mc.Parameters.AddWithValue("@inn", r.INN);
                mc.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
        }
        finally
        {
            CloseConnection();
        }
    }

    public void Update(Recruit r)
    {
        try
        {
            OpenConnection();
            string sql = "update recruits set FamilyName = @fname, Name = @name, FatherName = @fath, DateOfBirth = @bday, PhoneNumber = @number, Adress = @adres, Passport = @pass, SNILS = @snils, INN = @inn where Id = @id";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@id", r.Id);
                mc.Parameters.AddWithValue("@fname", r.FamilyName);
                mc.Parameters.AddWithValue("@name", r.Name);
                mc.Parameters.AddWithValue("@fath", r.FatherName);
                mc.Parameters.AddWithValue("@bday", r.DateOfBirth);
                mc.Parameters.AddWithValue("@number", r.PhoneNumber);
                mc.Parameters.AddWithValue("@adres", r.Adress);
                mc.Parameters.AddWithValue("@pass", r.Passport);
                mc.Parameters.AddWithValue("@snils", r.SNILS);
                mc.Parameters.AddWithValue("@inn", r.INN);
                mc.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
        }
        finally
        {
            CloseConnection();
        }
    }

    public void Delete(Recruit r)
    {
        try
        {
            OpenConnection();
            string sql = "delete from recruits where Id = @id";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@id", r.Id);
                mc.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
        }
        finally
        {
            CloseConnection();
        }
    }

    public int Count()
    {
        int res = 0;
        try
        {
            OpenConnection();
            string sql = "select count(*) as counted from recruits";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                using (var r = mc.ExecuteReader())
                {
                    if  (r.Read()) res = r.GetInt32("counted");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
        }
        finally
        {
            CloseConnection();
        }
        return res;
    }
}
