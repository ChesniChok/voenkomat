using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using Tmds.DBus.Protocol;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.DB;

public class EmployeeRepository : BaseRepository<Employee>
{
    private CabinetRepository _cabinetRepository;
    private JobRepository _jobRepository;
    public EmployeeRepository(AppSettings appSettings, CabinetRepository cabinetRepository, JobRepository jobRepository) : base(appSettings)
    {
        _cabinetRepository = cabinetRepository;
        _jobRepository = jobRepository;
    }

    public List<Employee> GetAll()
    {
        List<Employee> employees = new List<Employee>();
        try
        {
            OpenConnection();
            string sql = "SELECT * FROM employee";
            using (var mc = new MySqlCommand(sql, _connection))
            using (var dr = mc.ExecuteReader())
            {
                while (dr.Read())
                {
                    int cabinetNumber = dr.GetInt32("CabinetNumber");
                    int jobId = dr.GetInt32("JobId");
                    
                    employees.Add(new Employee(
                        dr.GetInt32("Id"),
                        dr.GetString("FullName"),
                        _cabinetRepository.GetById(cabinetNumber),
                        _jobRepository.GetById(jobId)
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
        return employees;
    }
    
    public List<Employee> GetPage(int offset, int limit)
    {
        List<Employee> cabinets = new();
        try
        {
            OpenConnection();
            string sql = "select * from emoloyees limit @limit offset @offset";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@limit", limit);
                mc.Parameters.AddWithValue("@offset", offset);
                using (var dr = mc.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        cabinets.Add(new Employee
                        (
                            dr.GetInt32("Id"),
                            dr.GetString("FullName"),
                            _cabinetRepository.GetById(dr.GetInt32("CabinetNumber")),
                            _jobRepository.GetById(dr.GetInt32("JobId")
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
            throw;
        }
        return cabinets;
    }

    public Employee GetById(int id)
    {
        Employee employee = new Employee();
        try
        {
            OpenConnection();
            string sql = "SELECT * FROM employee WHERE Id = @Id";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@Id", id);
                using (var dr = mc.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        int cabinetNumber = dr.GetInt32("CabinetNumber");
                        int jobId = dr.GetInt32("JobId");
                        
                        employee = new Employee(
                            dr.GetInt32("Id"),
                            dr.GetString("FullName"),
                            _cabinetRepository.GetById(cabinetNumber),
                            _jobRepository.GetById(jobId)
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
        return employee;
    }
}