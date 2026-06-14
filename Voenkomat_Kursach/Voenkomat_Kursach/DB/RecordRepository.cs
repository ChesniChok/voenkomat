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
    private RecruitRepository _recruitRepository;
    public RecordRepository(string connectionString, EmployeeRepository employeeRepository,
        MedComissionRepository medComissionRepository, RecruitRepository recruitRepository) : base(connectionString)
    {
        _employeeRepository = employeeRepository;
        _medComissionRepository = medComissionRepository;
        _recruitRepository = recruitRepository;
    }
    public List<Record> GetAll()
    {
            List<Record> documents = new List<Record>();
            try
            {
                OpenConnection();
                string sql = "SELECT * FROM documents where id = @id";
                using (var mc = new MySqlCommand(sql, _connection))
                using (var dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int medComiddionId = dr.GetInt32("MedComissionId");
                        int recruitId = dr.GetInt32("RecruitId");
                        int employeeId = dr.GetInt32("EmployeeId");
                        
                        documents.Add(new Record(
                            dr.GetInt32("Id"),
                            dr.GetString("Type"),
                            _employeeRepository.GetById(employeeId),
                            _medComissionRepository.GetById(medComiddionId),
                            _recruitRepository.GetById(recruitId),
                            dr.GetString("Content"),
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
            return documents;
    }

        public Record? GetById(int id)
        {
            Record record = new Record();
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM documents WHERE id = @id";
                using (var mc = new MySqlCommand(sql, _connection))
                {
                    mc.Parameters.AddWithValue("@Id", id);
                    using (var dr = mc.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            int medComiddionId = dr.GetInt32("MedComissionId");
                            int recruitId = dr.GetInt32("RecruitId");
                            int employeeId = dr.GetInt32("EmployeeId");
                            
                            record = new Record(
                            
                                dr.GetInt32("Id"),
                                dr.GetString("Type"),
                                _employeeRepository.GetById(employeeId),
                                _medComissionRepository.GetById(medComiddionId),
                                _recruitRepository.GetById(recruitId),
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
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return record;
        }
}
