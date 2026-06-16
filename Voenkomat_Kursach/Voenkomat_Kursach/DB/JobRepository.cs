using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.DB;

public class JobRepository : BaseRepository<Job>
{
    private List<Job> _jobs;
    public JobRepository(AppSettings appSettings) : base(appSettings)
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
                        jobs.Add(new Job(
                        
                            dr.GetInt32("Id"),
                            dr.GetString("Name")
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
            return jobs;
        }
        
        public List<Job> GetPage(int offset, int limit)
        {
            List<Job> jobs = new();
            try
            {
                OpenConnection();
                string sql = "select * from jobs limit @limit offset @offset";
                using (var mc = new MySqlCommand(sql, _connection))
                {
                    mc.Parameters.AddWithValue("@limit", limit);
                    mc.Parameters.AddWithValue("@offset", offset);
                    using (var dr = mc.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            jobs.Add(new Job
                            (
                                dr.GetInt32("Id"),
                                dr.GetString("Name")
                            ));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                CloseConnection();
                Console.WriteLine(e);
                throw;
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
                            job = new Job(
                            
                                dr.GetInt32("Id"),
                                dr.GetString("Name")
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
            return job;
        }
        
    public void Add(Job j)
    {
        try
        {
            OpenConnection();
            string sql = "insert into jobs values(@id, @name)";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@id", j.Id);
                mc.Parameters.AddWithValue("@name", j.Name);
                mc.ExecuteNonQuery();
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
    }

    public void Update(Job j)
    {
        try
        {
            OpenConnection();
            string sql = "update jobs set Name = @name where Id = @id";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@id", j.Id);
                mc.Parameters.AddWithValue("@name", j.Name);
                mc.ExecuteNonQuery();
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
    }

    public void Delete(Job j)
    {
        try
        {
            OpenConnection();
            string sql = "delete from jobs where Id = @id";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@id", j.Id);
                mc.ExecuteNonQuery();
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
    }
}
