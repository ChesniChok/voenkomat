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
    public RecordRepository(AppSettings appSettings, EmployeeRepository employeeRepository,
        MedComissionRepository medComissionRepository, RecruitRepository recruitRepository) : base(appSettings)
    {
        _employeeRepository = employeeRepository;
        _medComissionRepository = medComissionRepository;
        _recruitRepository = recruitRepository;
    }
    public List<Record> GetAll()
    {
            List<Record> records = new List<Record>();
            try
            {
                OpenConnection();
                string sql = "SELECT * FROM records where id = @id";
                using (var mc = new MySqlCommand(sql, _connection))
                using (var dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int medComiddionId = dr.GetInt32("MedComission_Id");
                        int recruitId = dr.GetInt32("Recruit_Id");
                        int employeeId = dr.GetInt32("Employee_Id");
                        
                        records.Add(new Record(
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
                _connection.Open();
                string sql = "SELECT * FROM records WHERE id = @id";
                using (var mc = new MySqlCommand(sql, _connection))
                {
                    mc.Parameters.AddWithValue("@Id", id);
                    using (var dr = mc.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            int medComiddionId = dr.GetInt32("Med_ComissionId");
                            int recruitId = dr.GetInt32("Recruit_Id");
                            int employeeId = dr.GetInt32("Employee_Id");
                            
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
                
            }
            finally
            {
                CloseConnection();
            }
            return record;
        }
}
