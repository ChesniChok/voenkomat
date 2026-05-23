using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.DB;

public class VisitRepository : BaseRepository<Visit>
{
    public VisitRepository(string connectionString) : base(connectionString)
    {
    }
    public List<Visit> GetAll()
        {
            List<Visit> visits = new List<Visit>();
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM Visit";
                using (var mc = new MySqlCommand(sql, _connection))
                using (var dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
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
            Visit visit = null;
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM Visit WHERE id = @id";
                using (var mc = new MySqlCommand(sql, _connection))
                {
                    mc.Parameters.AddWithValue("@Id", id);
                    using (var dr = mc.ExecuteReader())
                    {
                        if (dr.Read())
                        {
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
