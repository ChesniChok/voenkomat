using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.DB;

public class VisitRepository : BaseRepository<Visit>
{
    private RecruitRepository _recruitRepository;
    public VisitRepository(string connectionString, RecruitRepository recruitRepository) : base(connectionString)
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
                        int recruitId = dr.GetInt32("recruit");
                        Recruit recruit = _recruitRepository.GetById(recruitId);
                        
                        visits.Add(new Visit
                        {
                            Id = dr.GetInt32("Id"),
                            Date = dr.GetDateOnly("Date"),
                            OutTime = dr.GetTimeOnly("OutTime"),
                            InTime = dr.GetTimeOnly("InTime")
                        });
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
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
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
                            int recruitId = dr.GetInt32("recruit");
                            Recruit recruit = _recruitRepository.GetById(recruitId);
                            
                            visit = new Visit
                            {
                                Id = dr.GetInt32("Id"),
                                Date = dr.GetDateOnly("Date"),
                                OutTime = dr.GetTimeOnly("OutTime"),
                                InTime = dr.GetTimeOnly("InTime")
                            };
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
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
            return visit;
        }
}
