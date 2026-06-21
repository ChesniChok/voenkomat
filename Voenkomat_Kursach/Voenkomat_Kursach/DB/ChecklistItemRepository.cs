using System;
using System.Collections.Generic;
using MySqlConnector;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.DB;

public class ChecklistItemRepository : BaseRepository<ChecklistItem>
{

    private JobRepository _jr;

    public ChecklistItemRepository(AppSettings appSettings, JobRepository jr) : base(appSettings)
    {
        _jr = jr;
    }
    
    
    public List<ChecklistItem> GetAll()
    {
        List<ChecklistItem> checks = new List<ChecklistItem>();
        try
        {
            _connection.Open();
            string sql = "SELECT * FROM checkitems";
            using (var mc = new MySqlCommand(sql, _connection))
            using (var dr = mc.ExecuteReader())
            {
                while (dr.Read())
                {
                    checks.Add(new ChecklistItem(
                        dr.GetInt32("Id"),
                        _jr.GetById(dr.GetInt32("Job_Id")),
                        dr.GetString("Name"),
                        dr.GetString("Description")
                    ));
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
        }
        finally
        {
            CloseConnection();
        }
        return checks;
    }
    
    public List<ChecklistItem> GetAll(Job doc)
    {
        List<ChecklistItem> checks = new List<ChecklistItem>();
        try
        {
            OpenConnection();
            string sql = "SELECT * FROM checkitems where Job_Id  = @docid";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@docid", doc.Id);
                using (var dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        checks.Add(new ChecklistItem(
                            dr.GetInt32("Id"),
                            _jr.GetById(dr.GetInt32("Job_Id")),
                            dr.GetString("Name"),
                            dr.GetString("Description")
                        ));
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
        }
        finally
        {
            CloseConnection();
        }
        return checks;
    }
    
    public List<ChecklistItem> GetPage(int offset, int limit)
    {
        List<ChecklistItem> checks = new();
        try
        {
            OpenConnection();
            string sql = "select * from checkitems limit @limit offset @offset";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@limit", limit);
                mc.Parameters.AddWithValue("@offset", offset);
                using (var dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var jid = dr.GetOrdinal("Job_Id");
                        checks.Add(new ChecklistItem
                        (
                            dr.GetInt32("Id"),
                            dr.IsDBNull(jid) ? new(0, "") : _jr.GetById(dr.GetInt32("Job_Id")),
                            dr.GetString("Name"),
                            dr.GetString("Description")
                        ));
                    }
                }
            }
        }
        catch (Exception e)
        {
            CloseConnection();
            Console.WriteLine(e);
            
        }
        return checks;
    }

    public ChecklistItem GetById(int id)
    {
        ChecklistItem check = new ChecklistItem();
        try
        {
            _connection.Open();
            string sql = "SELECT * FROM checkitems WHERE id = @id";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@Id", id);
                using (var dr = mc.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        check = new ChecklistItem(
                            dr.GetInt32("Id"),
                            _jr.GetById(dr.GetInt32("Job_Id")),
                            dr.GetString("Name"),
                            dr.GetString("Description")
                        );
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
        }
        finally
        {
            CloseConnection();
        }
        return check;
    }
    
    public void Add(ChecklistItem c)
    {
        try
        {
            OpenConnection();
            string sql = "insert into checkitems values(@id, @jid, @name, @desc)";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@id", c.Id);
                mc.Parameters.AddWithValue("@jid", c.Doctor.Id);
                mc.Parameters.AddWithValue("@name", c.Name);
                mc.Parameters.AddWithValue("@desc", c.Description);
                mc.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
        }
        finally
        {
            CloseConnection();
        }
    }
    
    public void AddNull(ChecklistItem c)
    {
        try
        {
            OpenConnection();
            string sql = "insert into checkitems values(@id, @jid, @name, @desc)";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@id", c.Id);
                mc.Parameters.AddWithValue("@jid", DBNull.Value);
                mc.Parameters.AddWithValue("@name", c.Name);
                mc.Parameters.AddWithValue("@desc", c.Description);
                mc.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
        }
        finally
        {
            CloseConnection();
        }
    }

    public void Update(ChecklistItem c)
    {
        try
        {
            OpenConnection();
            string sql = "update checkitems set Job_Id = @jid, Name = @name, Description = @desc where Id = @id";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@id", c.Id);
                mc.Parameters.AddWithValue("@jid", c.Doctor.Id);
                mc.Parameters.AddWithValue("@name", c.Name);
                mc.Parameters.AddWithValue("@desc", c.Description);
                mc.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
        }
        finally
        {
            CloseConnection();
        }
    }

    public void Delete(ChecklistItem c)
    {
        try
        {
            OpenConnection();
            string sql = "delete from checkitems where Id = @id";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@id", c.Id);
                mc.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
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
            string sql = "select count(*) as counted from checkitems";
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
            
        }
        finally
        {
            CloseConnection();
        }
        return res;
    }
}