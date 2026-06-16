using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.DB;

public class RoleRepository : BaseRepository<Role>
{
    public RoleRepository(AppSettings appSettings) : base(appSettings)
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
                            dr.GetBoolean("Is_Med")
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
            List<Role> roles = new();
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
                        while (dr.Read())
                        {
                            roles.Add(new Role
                            (
                                dr.GetInt32("Id"),
                                dr.GetString("Name"),
                                dr.GetBoolean("Is_Med")
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
                            role = new Role(
                            
                                dr.GetInt32("Id"),
                                dr.GetString("Name"),
                                dr.GetBoolean("Is_Med")
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
        
    public void Add(Role r)
    {
        try
        {
            OpenConnection();
            string sql = "insert into roles values(@id, @name, @im)";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@id", r.Id);
                mc.Parameters.AddWithValue("@name", r.Name);
                mc.Parameters.AddWithValue("@im", r.IsMed);
                mc.ExecuteNonQuery();
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
    }

    public void Update(Role r)
    {
        try
        {
            OpenConnection();
            string sql = "update roles set Name = @name, Is_Med = @im where Id = @id";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@id", r.Id);
                mc.Parameters.AddWithValue("@name", r.Name);
                mc.Parameters.AddWithValue("@im", r.IsMed);
                mc.ExecuteNonQuery();
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
    }

    public void Delete(Role r)
    {
        try
        {
            OpenConnection();
            string sql = "delete from roles where Id = @id";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@id", r.Id);
                mc.ExecuteNonQuery();
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
    }

    public int Count()
    {
        int res = 0;
        try
        {
            OpenConnection();
            string sql = "select count(*) as counted from roles";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                using (var r = mc.ExecuteReader())
                {
                    if  (r.Read()) res = r.GetInt32("counted");
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
        return res;
    }
}


     