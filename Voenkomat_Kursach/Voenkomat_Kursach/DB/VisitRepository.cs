using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.DB;

public class VisitRepository : BaseRepository<Visit>
{
    private RecruitRepository _recruitRepository;
    public VisitRepository(AppSettings appSettings, RecruitRepository recruitRepository) : base(appSettings)
    {
        _recruitRepository = recruitRepository;
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
                            _recruitRepository.GetById(dr.GetInt32("RecruitId")),
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
                throw;
            }
            finally
            {
                CloseConnection();
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
                                _recruitRepository.GetById(dr.GetInt32("RecruitId")),
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
                throw;
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
                                _recruitRepository.GetById(dr.GetInt32("RecruitId")),
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
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return visits;
        }
}
