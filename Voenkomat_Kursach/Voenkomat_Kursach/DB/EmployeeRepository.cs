using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
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
                _connection.Open();
                string sql = "SELECT * FROM employee";
                using (var mc = new MySqlCommand(sql, _connection))
                using (var dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int cabinetNumber = dr.GetInt32("cabinet");
                        Cabinet cabinet = _cabinetRepository.GetById(cabinetNumber);
                        int jobId = dr.GetInt32("job");
                        Job job = _jobRepository.GetById(jobId);
                        
                        employees.Add(new Employee
                        {
                            Id = dr.GetInt32("Id"),
                            FullName = dr.GetString("FullName")
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
            return employees;
        }

        public Employee GetById(int id)
        {
            Employee employee = new Employee();
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM employee WHERE id = @id";
                using (var mc = new MySqlCommand(sql, _connection))
                {
                    mc.Parameters.AddWithValue("@Id", id);
                    using (var dr = mc.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            int cabinetNumber = dr.GetInt32("cabinet");
                            Cabinet cabinet = _cabinetRepository.GetById(cabinetNumber);
                            int jobId = dr.GetInt32("job");
                            Job job = _jobRepository.GetById(jobId);
                            
                            employee = new Employee
                            {
                                Id = dr.GetInt32("Id"),
                                FullName = dr.GetString("FullName")
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
            return employee;
        }
}