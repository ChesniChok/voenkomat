using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.DB;

public class DocumentRepository : BaseRepository<Document>
{
    private EmployeeRepository _employeeRepository;
    private MedComissionRepository _medComissionRepository;
    private RecruitRepository _recruitRepository;
    public DocumentRepository(string connectionString, EmployeeRepository employeeRepository,
        MedComissionRepository medComissionRepository, RecruitRepository recruitRepository) : base(connectionString)
    {
        _employeeRepository = employeeRepository;
        _medComissionRepository = medComissionRepository;
        _recruitRepository = recruitRepository;
    }
    public List<Document> GetAll()
        {
            List<Document> documents = new List<Document>();
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM documents";
                using (var mc = new MySqlCommand(sql, _connection))
                using (var dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int employeeId = dr.GetInt32("employee");
                        Employee employee = _employeeRepository.GetById(employeeId);
                        int medComissionId = dr.GetInt32("medComission");
                        MedComission medComission = _medComissionRepository.GetById(medComissionId);
                        int recruitId = dr.GetInt32("recruit");
                        Recruit recruit = _recruitRepository.GetById(recruitId);
                        
                        documents.Add(new Document
                        {
                            Id = dr.GetInt32("Id"),
                            Type = dr.GetString("Type"),
                            Number = dr.GetString("Number"),
                            FileName = dr.GetString("FileName"),
                            Description = dr.GetString("Description")
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
            return documents;
        }

        public Document GetById(int id)
        {
            Document document = new Document();
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
                            int employeeId = dr.GetInt32("employee");
                            Employee employee = _employeeRepository.GetById(employeeId);
                            int medComissionId = dr.GetInt32("medComission");
                            MedComission medComission = _medComissionRepository.GetById(medComissionId);
                            int recruitId = dr.GetInt32("recruit");
                            Recruit recruit = _recruitRepository.GetById(recruitId);
                            
                            document = new Document
                            {
                                Id = dr.GetInt32("Id"),
                                Type = dr.GetString("Type"),
                                Number = dr.GetString("Number"),
                                FileName = dr.GetString("FileName"),
                                Description = dr.GetString("Description")
                            };
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
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
            return document;
        }
}
