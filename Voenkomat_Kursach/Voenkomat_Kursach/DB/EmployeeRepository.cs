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
    public EmployeeRepository(string connectionString, CabinetRepository cabinetRepository, JobRepository jobRepository) : base(connectionString)
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

    public Employee? GetById(int id)
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