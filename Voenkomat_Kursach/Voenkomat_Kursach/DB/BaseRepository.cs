using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;

namespace Voenkomat_Kursach.DB;

public abstract class BaseRepository<T> : IRepository<T> where T : class
{
    protected MySqlConnection _connection;
    protected string _connectionString;

    public BaseRepository(string connectionString)
    {
        _connectionString = connectionString;
        _connection = new MySqlConnection(_connectionString);
    }

    public bool OpenConnection()
    {
        try
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool CloseConnection()
    {
        try
        {
            if (_connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public List<T> GetAll()
    {
        throw new NotImplementedException();
    }

    public T GetById(int id)
    {
        throw new NotImplementedException();
    }
}