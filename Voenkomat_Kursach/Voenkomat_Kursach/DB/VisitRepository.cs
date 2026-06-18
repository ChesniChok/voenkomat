using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.DB;

public class VisitRepository : BaseRepository<Visit>
{
    private MedComissionRepository _medComissionRepository;
    public VisitRepository(AppSettings appSettings, MedComissionRepository medComissionRepository) : base(appSettings)
    {
        _medComissionRepository = medComissionRepository;
    }
    public List<Visit> GetAll()
        {
            List<Visit> visits = new List<Visit>();
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM visits";
                using (var mc = new MySqlCommand(sql, _connection))
                using (var dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        visits.Add(new Visit(
                            dr.GetInt32("Id"),
                            _medComissionRepository.GetById(dr.GetInt32("Medcomission_Id")),
                            dr.GetDateOnly("Date"),
                            dr.GetTimeOnly("InTime"),
                            dr.GetString("Goal"),
                            dr.GetTimeOnly("OutTime")
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
            return visits;
        }
    
    public List<Visit> GetPage(int offset, int limit, MedComission m)
    {
        List<Visit> visits = new();
        try
        {
            OpenConnection();
            string sql = "select * from visits where Medcomission_Id = @mid limit @limit offset @offset";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@limit", limit);
                mc.Parameters.AddWithValue("@offset", offset);
                mc.Parameters.AddWithValue("@mid", m.Id);
                using (var dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        visits.Add(new Visit
                        (
                            dr.GetInt32("Id"),
                            _medComissionRepository.GetById(dr.GetInt32("Medcomission_Id")),
                            dr.GetDateOnly("Date"),
                            dr.GetTimeOnly("InTime"),
                            dr.GetString("Goal"),
                            dr.GetTimeOnly("OutTime")
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
        return visits;
    }

        public Visit GetById(int id)
        {
            Visit visit = new Visit();
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM visits WHERE id = @id";
                using (var mc = new MySqlCommand(sql, _connection))
                {
                    mc.Parameters.AddWithValue("@Id", id);
                    using (var dr = mc.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            visit = new Visit(
                                dr.GetInt32("Id"),
                                _medComissionRepository.GetById(dr.GetInt32("Medcomission_Id")),
                                dr.GetDateOnly("Date"),
                                dr.GetTimeOnly("InTime"),
                                dr.GetString("Goal"),
                                dr.GetTimeOnly("OutTime")
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
            return visit;
        }
        public List<Visit> GetVisitsToday()
        {
            List<Visit> visits = new List<Visit>();
            try
            {
                OpenConnection();
                
                DateOnly today = DateOnly.FromDateTime(DateTime.Today);
                TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);
                
                string sql = @"SELECT * FROM visits WHERE Date = @today AND OutTime > @currentTime";
                using (var mc = new MySqlCommand(sql, _connection))
                {
                    mc.Parameters.AddWithValue("@today", today);
                    mc.Parameters.AddWithValue("@currentTime", currentTime);
            
                    using (var dr = mc.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            visits.Add(new Visit(
                                dr.GetInt32("Id"),
                                _medComissionRepository.GetById(dr.GetInt32("Medcomission_Id")),
                                dr.GetDateOnly("Date"),
                                dr.GetTimeOnly("InTime"),
                                dr.GetString("Goal"),
                                dr.GetTimeOnly("OutTime")
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
            return visits;
        }
        
        public List<Visit> GetPageToday(int offset, int limit)
        {
            List<Visit> visits = new();
            try
            {
                OpenConnection();
                var date = DateOnly.FromDateTime(DateTime.Now);
                string sql = "select * from visits where Date = @today limit @limit offset @offset";
                using (var mc = new MySqlCommand(sql, _connection))
                {
                    mc.Parameters.AddWithValue("@limit", limit);
                    mc.Parameters.AddWithValue("@offset", offset);
                    mc.Parameters.AddWithValue("@today", date);
                    using (var dr = mc.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            visits.Add(new Visit
                            (
                                dr.GetInt32("Id"),
                                _medComissionRepository.GetById(dr.GetInt32("Medcomission_Id")),
                                dr.GetDateOnly("Date"),
                                dr.GetTimeOnly("InTime"),
                                dr.GetString("Goal"),
                                dr.GetTimeOnly("OutTime")
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
            return visits;
        }
        
    public void Add(Visit v)
    {
        try
        {
            OpenConnection();
            string sql = "insert into visits values(0, @mid, @date, @int, @goal, @out)";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@mid", v.MedComission.Id);
                mc.Parameters.AddWithValue("@date", v.Date);
                mc.Parameters.AddWithValue("@int", v.InTime);
                mc.Parameters.AddWithValue("@goal", v.Goal);
                mc.Parameters.AddWithValue("@out", v.OutTime);
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

    public void Update(Visit v)
    {
        try
        {
            OpenConnection();
            string sql = "update visits set @mid, @date, @int, @goal, @out where Id = @id";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@mid", v.Id);
                mc.Parameters.AddWithValue("@mid", v.MedComission.Id);
                mc.Parameters.AddWithValue("@date", v.Date);
                mc.Parameters.AddWithValue("@int", v.InTime);
                mc.Parameters.AddWithValue("@goal", v.Goal);
                mc.Parameters.AddWithValue("@out", v.OutTime);
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

    public void Delete(Visit v)
    {
        try
        {
            OpenConnection();
            string sql = "delete from visits where Id = @id";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@id", v.Id);
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
            string sql = "select count(*) as counted from visits";
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
