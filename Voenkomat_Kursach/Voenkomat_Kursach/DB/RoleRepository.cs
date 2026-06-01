using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.DB;

public class RoleRepository : BaseRepository<Role>
{
    public RoleRepository(string connectionString) : base(connectionString)
    {
        
    }

        public List<Role> GetAll()
        {
            List<Role> roles = new List<Role>();
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM roles";
                using (var mc = new MySqlCommand(sql, _connection))
                using (var dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        roles.Add(new Role
                        {
                            Id = dr.GetInt32("Id"),
                            Name = dr.GetString("Name")
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
                CloseConnection();
            }
            return roles;
        }

        public Role GetById(int id)
        {
            Role role = new Role();
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM roles WHERE id = @id";
                using (var mc = new MySqlCommand(sql, _connection))
                {
                    mc.Parameters.AddWithValue("@Id", id);
                    using (var dr = mc.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            role = new Role
                            {
                                Id = dr.GetInt32("Id"),
                                Name = dr.GetString("Name")
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
                CloseConnection();
            }
            return role;
        }
}


     