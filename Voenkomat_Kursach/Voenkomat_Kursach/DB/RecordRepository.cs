using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.DB;

public class RecordRepository : BaseRepository<Record>
{
    private EmployeeRepository _employeeRepository;
    private MedComissionRepository _medComissionRepository;
    public RecordRepository(AppSettings appSettings, EmployeeRepository employeeRepository,
        MedComissionRepository medComissionRepository) : base(appSettings)
    {
        _employeeRepository = employeeRepository;
        _medComissionRepository = medComissionRepository;
    }
    public List<Record> GetAll()
    {
            List<Record> records = new List<Record>();
            try
            {
                OpenConnection();
                string sql = "SELECT * FROM records";
                using (var mc = new MySqlCommand(sql, _connection))
                using (var dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int medComiddionId = dr.GetInt32("MedComission_Id");
                        int employeeId = dr.GetInt32("Employee_Id");
                        
                        records.Add(new Record(
                            dr.GetInt32("Id"),
                            dr.GetString("Type"),
                            _employeeRepository.GetById(employeeId),
                            _medComissionRepository.GetById(medComiddionId),
                            dr.GetString("Content"),
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
            return records;
    }
    
    public List<Record> GetAllTyped(string type)
    {
        List<Record> records = new List<Record>();
        try
        {
            OpenConnection();
            string sql = "SELECT * FROM records where Type = @type";
            using (var mc = new MySqlCommand(sql, _connection))
            using (var dr = mc.ExecuteReader())
            {
                while (dr.Read())
                {
                    int medComiddionId = dr.GetInt32("MedComission_Id");
                    int employeeId = dr.GetInt32("Employee_Id");
                        
                    records.Add(new Record(
                        dr.GetInt32("Id"),
                        dr.GetString("Type"),
                        _employeeRepository.GetById(employeeId),
                        _medComissionRepository.GetById(medComiddionId),
                        dr.GetString("Content"),
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
        return records;
    }

        public Record? GetById(int id)
        {
            Record record = new Record();
            try
            {
                OpenConnection();
                string sql = "SELECT * FROM records WHERE id = @id";
                using (var mc = new MySqlCommand(sql, _connection))
                {
                    mc.Parameters.AddWithValue("@Id", id);
                    using (var dr = mc.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            int medComiddionId = dr.GetInt32("Med_ComissionId");
                            int employeeId = dr.GetInt32("Employee_Id");
                            
                            record = new Record(
                            
                                dr.GetInt32("Id"),
                                dr.GetString("Type"),
                                _employeeRepository.GetById(employeeId),
                                _medComissionRepository.GetById(medComiddionId),
                                dr.GetString("Content"),
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
            return record;
        }
}
