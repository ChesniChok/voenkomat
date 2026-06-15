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
                        dr.GetInt32("id"),
                        _jr.GetById(dr.GetInt32("job_id")),
                        dr.GetString("Name"),
                        dr.GetString("Description")
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
        return checks;
    }
    
    public List<ChecklistItem> GetPage(int offset, int limit)
    {
        List<ChecklistItem> cabinets = new();
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
                    if (dr.Read())
                    {
                        cabinets.Add(new ChecklistItem
                        (
                            dr.GetInt32("id"),
                            _jr.GetById(dr.GetInt32("job_id")),
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
            throw;
        }
        return cabinets;
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
                            dr.GetInt32("id"),
                            _jr.GetById(dr.GetInt32("job_id")),
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
            throw;
        }
        finally
        {
            CloseConnection();
        }
        return check;
    }
    
}