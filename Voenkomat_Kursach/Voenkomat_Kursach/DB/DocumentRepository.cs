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
                        
                        documents.Add(new Document(
                            dr.GetInt32("Id"),
                            dr.GetString("Type"),
                            dr.GetString("Number"),
                            dr.GetString("FileName"),
                            _employeeRepository.GetById(employeeId),
                            _medComissionRepository.GetById(medComiddionId),
                            _recruitRepository.GetById(recruitId),
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

        public Document? GetById(int id)
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
                            int medComiddionId = dr.GetInt32("MedComissionId");
                            int recruitId = dr.GetInt32("RecruitId");
                            int employeeId = dr.GetInt32("EmployeeId");
                            
                            document = new Document(
                            
                                dr.GetInt32("Id"),
                                dr.GetString("Type"),
                                dr.GetString("Number"),
                                dr.GetString("FileName"),
                                _employeeRepository.GetById(employeeId),
                                _medComissionRepository.GetById(medComiddionId),
                                _recruitRepository.GetById(recruitId),
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
            return document;
        }
}
