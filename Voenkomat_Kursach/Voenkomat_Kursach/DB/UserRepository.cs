using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.DB;

public class UserRepository : BaseRepository<User>
{
    private RoleRepository _roleRepository;
    private EmployeeRepository _employeeRepository;
    public UserRepository(AppSettings appSettings, RoleRepository roleRepository,EmployeeRepository employeeRepository) : base(appSettings)
    {
        _roleRepository = roleRepository;
        _employeeRepository = employeeRepository;
    }

    public List<User> GetAll()
    {
        List<User> users = new List<User>();
        try
        {
            OpenConnection();
            string sql = "SELECT * FROM users";
            using (var mc = new MySqlCommand(sql, _connection))
            using (var dr = mc.ExecuteReader())
            {
                while (dr.Read())
                {
                    int employeeId = dr.GetInt32("Employee_Id");
                    int roleId = dr.GetInt32("Role_Id");
                    
                    users.Add(new User(
                        dr.GetInt32("Id"),
                        _employeeRepository.GetById(employeeId),
                        dr.GetString("Login"),
                        dr.GetString("Password"),
                        _roleRepository.GetById(roleId)
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
        return users;
    }
    
    public List<User> GetPage(int offset, int limit)
    {
        List<User> users = new();
        try
        {
            OpenConnection();
            string sql = "select * from users limit @limit offset @offset";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@limit", limit);
                mc.Parameters.AddWithValue("@offset", offset);
                using (var dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        users.Add(new User
                        (
                            dr.GetInt32("Id"),
                            _employeeRepository.GetById(dr.GetInt32("EmployeeId")),
                            dr.GetString("Login"),
                            dr.GetString("Password"),
                            _roleRepository.GetById(dr.GetInt32("RoleId"))
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
        return users;
    }

        public User GetById(int id)
        {
            User user = new User();
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM users WHERE id = @id";
                using (var mc = new MySqlCommand(sql, _connection))
                {
                    mc.Parameters.AddWithValue("@Id", id);
                    using (var dr = mc.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            int employeeId = dr.GetInt32("EmployeeId");
                            int roleId = dr.GetInt32("RoleId");
                            user = new User(
                            
                                dr.GetInt32("Id"),
                                _employeeRepository.GetById(employeeId),
                                dr.GetString("Login"),
                                dr.GetString("Password"),
                                _roleRepository.GetById(roleId)
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
            return user;
        }
}
