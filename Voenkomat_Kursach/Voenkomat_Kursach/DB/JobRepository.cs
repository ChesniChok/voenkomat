using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.DB;

public class JobRepository : BaseRepository<Job>
{

    public JobRepository(string connectionString) : base(connectionString)
    {
    }

        public List<Job> GetAll()
        {
            List<Job> jobs = new List<Job>();
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM jobs";
                using (var mc = new MySqlCommand(sql, _connection))
                using (var dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        jobs.Add(new Job
                        {
                            Id = dr.GetInt32("id"),
                            Name = dr.GetString("Name")
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
            return jobs;
        }

        public Job GetById(int id)
        {
            Job job = new Job();
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM jobs WHERE id = @id";
                using (var mc = new MySqlCommand(sql, _connection))
                {
                    mc.Parameters.AddWithValue("@Id", id);
                    using (var dr = mc.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            job = new Job
                            {
                                Id = dr.GetInt32("Id"),
                                Name = dr.GetString("Name")
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
            return job;
        }
}
