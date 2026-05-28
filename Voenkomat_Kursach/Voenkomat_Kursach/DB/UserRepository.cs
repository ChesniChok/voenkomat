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
    private List<User> _users;
    public UserRepository(string connectionString, RoleRepository roleRepository,EmployeeRepository employeeRepository) : base(connectionString)
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
            string sql = "SELECT * FROM user";
            using (var mc = new MySqlCommand(sql, _connection))
            using (var dr = mc.ExecuteReader())
            {
                while (dr.Read())
                {
                    int employeeId = dr.GetInt32("EmployeeId");
                    int roleId = dr.GetInt32("RoleId");
                    
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

        public User GetById(int id)
        {
            User user = new User();
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM user WHERE id = @id";
                using (var mc = new MySqlCommand(sql, _connection))
                {
                    mc.Parameters.AddWithValue("@Id", id);
                    using (var dr = mc.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            user = new User
                            {
                                Id = dr.GetInt32("Id"),
                                Login = dr.GetString("Login"),
                                Password = dr.GetString("Password")
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
            return user;
        }
}
