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
    
    public List<Record> GetAllTyped(MedComission m, string type)
    {
        List<Record> records = new List<Record>();
        try
        {
            OpenConnection();
            string sql = "select \n\t*\nfrom\n\trecords r \n\tjoin \n\tmedcomissions m \n\ton\n\t\tr.MedComission_Id = m.Id\nwhere \n\tm.Id = @mid\n\tand \n\tr.type = @typ\n;";
            using (var mc = new MySqlCommand(sql, _connection))
            using (var dr = mc.ExecuteReader())
            {
                mc.Parameters.AddWithValue("@mid", m.Id);
                mc.Parameters.AddWithValue("@typ", type);
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
    
    public List<Record> GetAllMed(MedComission m)
    {
        List<Record> records = new List<Record>();
        try
        {
            OpenConnection();
            string sql = "select \n\t*\nfrom\n\trecords r \n\tjoin \n\tmedcomissions m \n\ton\n\t\tr.MedComission_Id = m.Id\nwhere \n\tm.Id = @mid\n;";
            using (var mc = new MySqlCommand(sql, _connection))
            using (var dr = mc.ExecuteReader())
            {
                mc.Parameters.AddWithValue("@mid", m.Id);
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
    
    public List<Record> GetAllTyped(string type, Job doctor)
    {
        List<Record> records = new List<Record>();
        try
        {
            OpenConnection();
            string sql = "select \n\t* \nfrom \n\trecords r \n\tjoin \n\temployees e  \n\ton\n\t\tr.Author_Id = e.Id \n\tjoin\n\tjobs j \n\ton\n\t\te.Job_Id = j.Id\nwhere \n\tr.Type = @type\n\tand\n\tj.Id = @jid\n;";
            using (var mc = new MySqlCommand(sql, _connection))
            using (var dr = mc.ExecuteReader())
            {
                mc.Parameters.AddWithValue("@type", type);
                mc.Parameters.AddWithValue("@jid", doctor.Id);
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
