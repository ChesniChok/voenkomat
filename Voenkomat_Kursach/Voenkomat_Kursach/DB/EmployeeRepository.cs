using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.DB;

public class EmployeeRepository : BaseRepository<Employee>
{
    public EmployeeRepository(string connectionString) : base(connectionString)
    {
    }

    public List<Employee> GetAll()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM Employee";
                using (var mc = new MySqlCommand(sql, _connection))
                using (var dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
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
            Employee employee = null;
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM Employee WHERE id = @id";
                using (var mc = new MySqlCommand(sql, _connection))
                {
                    mc.Parameters.AddWithValue("@Id", id);
                    using (var dr = mc.ExecuteReader())
                    {
                        if (dr.Read())
                        {
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