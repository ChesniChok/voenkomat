using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.DB;

public class CabinetRepository : BaseRepository<Cabinet>
{
    public CabinetRepository(AppSettings appSettings) : base(appSettings)
    {
        
    }

    public List<Cabinet> GetAll()
    {
        List<Cabinet> cabinets = new List<Cabinet>();
        try
        {
            _connection.Open();
            string sql = "select * from cabinets";
            using (var mc = new MySqlCommand(sql, _connection))
            using (var dr = mc.ExecuteReader())
            {
                while (dr.Read())
                {
                    cabinets.Add(new Cabinet(
                    
                        dr.GetInt32("Number"),
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
        return cabinets;
    }

    public List<Cabinet> GetPage(int offset, int limit)
    {
        List<Cabinet> cabinets = new();
        try
        {
            OpenConnection();
            string sql = "select * from cabinets limit @limit offset @offset";
            using (var mc = new MySqlCommand(sql, _connection))
            {
                mc.Parameters.AddWithValue("@limit", limit);
                mc.Parameters.AddWithValue("@offset", offset);
                using (var dr = mc.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        cabinets.Add(new Cabinet
                        (
                            dr.GetInt32("Number"),
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

    public Cabinet GetById()
    {
        Cabinet cabinet = new Cabinet();
        try
        {
            _connection.Open();
            string sql = "select * from cabinets";
            using (var mc = new MySqlCommand(sql, _connection))
            using (var dr = mc.ExecuteReader())
            {
                while (dr.Read())
                {
                    cabinet = new Cabinet(
                    
                        dr.GetInt32("Number"),
                        dr.GetString("Name"),
                        dr.GetString("Description")
                    );
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
        return cabinet;
    }
}