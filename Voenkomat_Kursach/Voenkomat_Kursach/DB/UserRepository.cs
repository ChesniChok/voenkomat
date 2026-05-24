using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.DB;

public class UserRepository : BaseRepository<User>
{
    private RoleRepository _roleRepository;
    private List<User> _users;
    public UserRepository(string connectionString, RoleRepository roleRepository) : base(connectionString)
    {
        _roleRepository = roleRepository;
        _users = new List<User>();
        GetAll();
    }

        public List<User> GetAll()
        {
            List<User> users = new List<User>();
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM user";
                using (var mc = new MySqlCommand(sql, _connection))
                using (var dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int roleId = dr.GetInt32("role");
                        Role role = _roleRepository.GetById(roleId);
                        
                        users.Add(new User
                        {
                            Id = dr.GetInt32("Id"),
                            Login = dr.GetString("Login"),
                            Password = dr.GetString("Password")
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
                            int roleId = dr.GetInt32("role");
                            Role role = _roleRepository.GetById(roleId);
                            
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
