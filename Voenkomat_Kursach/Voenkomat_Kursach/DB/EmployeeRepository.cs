using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using Tmds.DBus.Protocol;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.DB;

public class EmployeeRepository : BaseRepository<Employee>
{
    
    private JobRepository _jobRepository;
    public EmployeeRepository(AppSettings appSettings, JobRepository jobRepository) : base(appSettings)
    {
        _jobRepository = jobRepository;
    }

    public List<Employee> GetAll()
    {
        List<Employee> employees = new List<Employee>();
        try
        {
            OpenConnection();
            string sql = "SELECT * FROM employees";
            using (var mc = new MySqlCommand(sql, _connection))
            using (var dr = mc.ExecuteReader())
            {
                while (dr.Read())
                {
                    int cabinetNumber = dr.GetInt32("Cabinet_Number");
                    int jobId = dr.GetInt32("Job_Id");
                    
                    employees.Add(new Employee(
                        dr.GetInt32("Id"),
                        dr.GetString("FullName"),
                        _jobRepository.GetById(jobId)
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
        return employees;
    }
    
    public List<Employee> GetPage(int offset, int limit)
    {
        List<Employee> employees = new();
        try
        {
            OpenConnection();
            string sql = "select * from employees limit @limit offset @offset";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@limit", limit);
                mc.Parameters.AddWithValue("@offset", offset);
                using (var dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        employees.Add(new Employee
                        (
                            dr.GetInt32("Id"),
                            dr.GetString("FullName"),
                            _jobRepository.GetById(dr.GetInt32("Job_Id")
                            )
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
        return employees;
    }

    public Employee GetById(int id)
    {
        Employee employee = new Employee();
        try
        {
            OpenConnection();
            string sql = "SELECT * FROM employees WHERE Id = @Id";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@Id", id);
                using (var dr = mc.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        int jobId = dr.GetInt32("Job_Id");
                        
                        employee = new Employee(
                            dr.GetInt32("Id"),
                            dr.GetString("FullName"),
                            _jobRepository.GetById(jobId)
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
        return employee;
    }
    
    public void Add(Employee e)
    {
        try
        {
            OpenConnection();
            string sql = "insert into employees values(@id, @fname, @jid)";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@id", e.Id);
                mc.Parameters.AddWithValue("@fname", e.FullName);
                mc.Parameters.AddWithValue("@jid", e.Job.Id);
                mc.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            
        }
        finally
        {
            CloseConnection();
        }
    }

    public void Update(Employee e)
    {
        try
        {
            OpenConnection();
            string sql = "update employees set FullName = @fname, Job_Id = @jid where Id = @id";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@id", e.Id);
                mc.Parameters.AddWithValue("@fname", e.FullName);
                mc.Parameters.AddWithValue("@jid", e.Job.Id);
                mc.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            
        }
        finally
        {
            CloseConnection();
        }
    }

    public void Delete(Employee e)
    {
        try
        {
            OpenConnection();
            string sql = "delete from employees where Id = @id";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@id", e.Id);
                mc.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            
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
            string sql = "select count(*) as counted from employees";
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