using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.DB;

public class UserRepository : BaseRepository<User>
{
    public UserRepository(string connectionString) : base(connectionString)
    {
    }

        public List<User> GetAll()
        {
            List<User> users = new List<User>();
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM User";
                using (var mc = new MySqlCommand(sql, _connection))
                using (var dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
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
            User user = null;
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM User WHERE id = @id";
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
