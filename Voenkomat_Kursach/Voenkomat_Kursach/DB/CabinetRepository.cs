using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.DB;

public class CabinetRepository : BaseRepository<Cabinet>
{
    private List<Cabinet> _cabinets;
    public CabinetRepository(string connectionString) : base(connectionString)
    {
        _cabinets = new List<Cabinet>();
        GetAll();
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
                    cabinets.Add(new Cabinet
                    {
                        Description = dr.GetString("Description"),
                        Name = dr.GetString("Name"),
                        Number = dr.GetInt32("Number")
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
                    cabinet = new Cabinet()
                    {
                        Description = dr.GetString("Description"),
                        Name = dr.GetString("Name"),
                        Number = dr.GetInt32("Number")
                    };
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        return cabinet;
    }
}