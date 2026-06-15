using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.DB;

public abstract class BaseRepository<T> : IRepository<T> where T : class
{
    protected MySqlConnection _connection;
    protected string _connectionString;
    private bool _disposed = false;

    public BaseRepository(AppSettings appSettings)
    {
        _connectionString = appSettings.ConnectionString;
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
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                if (_connection != null)
                {
                    if (_connection.State != ConnectionState.Closed)
                    {
                        _connection.Close();
                    }
                    _connection.Dispose();
                    _connection = null;
                }
            }
            _disposed = true;
        }
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    public List<T> GetAll()
    {
        throw new NotImplementedException();
    }

    public List<T> GetPage(int offset, int limit)
    {
        throw new NotImplementedException();
    }

    public T GetById(int id)
    {
        throw new NotImplementedException();
    }
}