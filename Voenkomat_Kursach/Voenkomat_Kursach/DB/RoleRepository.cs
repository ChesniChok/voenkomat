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
                        roles.Add(new Role(
                        
                            dr.GetInt32("Id"),
                            dr.GetString("Name"),
                            dr.GetBoolean("IsMed")
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
            return roles;
        }
        
        public List<Role> GetPage(int offset, int limit)
        {
            List<Role> cabinets = new();
            try
            {
                OpenConnection();
                string sql = "select * from roles limit @limit offset @offset";
                using (var mc = new MySqlCommand(sql, _connection))
                {
                    mc.Parameters.AddWithValue("@limit", limit);
                    mc.Parameters.AddWithValue("@offset", offset);
                    using (var dr = mc.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            cabinets.Add(new Role
                            (
                                dr.GetInt32("Id"),
                                dr.GetString("Name"),
                                dr.GetBoolean("IsMed")
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
                            role = new Role(
                            
                                dr.GetInt32("Id"),
                                dr.GetString("Name"),
                                dr.GetBoolean("IsMed")
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
            return role;
        }
}


     